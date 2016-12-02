Write-Host "Installing functions"
function RemoveSpecialChars($word)
{
    $toRemove = @(",", " ", ")")
    $toReplace = @("(")
    
    #Remove
    for($i = 0; $i -lt $toRemove.Length; $i++){
        $char = $toRemove[$i]
        $word = $word -replace [regex]::Escape($char), ""
    }

    #Replace
    for($i = 0; $i -lt $toReplace.Length; $i++){
        $char = $toReplace[$i]
        $word = $word -replace [regex]::Escape($char), "_"
    }

    return $word
}

function GetSQLDataType($mainframeDataType){
    
    $dataType1
    $sizeType1
    $dataType1Done = $false
    $sizeType1Done = $false
    $dataType2
    $sizeType2
    $dataType2Done = $false
    $sizeType2Done = $false
    
    for($i = 0; $i -lt $mainframeDataType.Length; $i++){
        if($mainframeDataType.Substring($i, 1) -eq "("){
            if($dataType1Done){
                $dataType2Done = $true
            } else {
                $dataType1Done = $true
            }
        }
        elseif($mainframeDataType.Substring($i, 1) -eq ")"){
            if($sizeType1Done){
                $sizeType2Done = $true
            } else {
                $sizeType1Done = $true
            }
        } else {
            if(!$dataType1Done){
                $dataType1 += $mainframeDataType.Substring($i, 1)
            } elseif(!$sizeType1Done){
                $sizeType1 += $mainframeDataType.Substring($i, 1)
            } elseif(!$dataType2Done){
                $dataType2 += $mainframeDataType.Substring($i, 1)
            } elseif(!$sizeType2Done){
                $sizeType2 += $mainframeDataType.Substring($i, 1)
            }
        }
    }

    $sqlDataType

    #S9(15)V99
    #S999V9(4)
    #S999V99
    #S999
    switch ($dataType1.ToUpper())
    {
        "XXX" {$sqlDataType += "varchar(MAX)"}
        "XX" {$sqlDataType += "varchar(MAX)"}
        "X" {$sqlDataType += ("varchar({0})" -f [int]$sizeType1)}
        "9" {$sqlDataType += ("varchar({0})" -f [int]$sizeType1)}
        "S9" { 
            if(!$dataType2Done){
                $sqlDataType += ("varchar({0})" -f [int]$sizeType1)
            } else {
                switch ($dataType2.ToUpper())
                {
                    "V9" {$sqlDataType += ("decimal({0},{1})" -f [int]$sizeType1, [int]$sizeType2)}
                    "V99" {}
                }
            }
        }
    }

    return $sqlDataType
}

function buildSQLCreateProcedureStatement($isInitial, $statement, $valueToAdd){
    if($isInitial){
        $statement = "CREATE PROCEDURE"
        $statement += (" spGet{0} `n" -f $tableName)
        $statement += (" @startAt Datetime, `n")
        $statement += (" @endAt Datetime `n")   
        $statement += "AS `n"
        $statement += "BEGIN `n"
        $statement += "SELECT "
    } else {
        $statement += ("`n`t`t {0}," -f $valueToAdd) 
    }

    return $statement
}

function clearEndOfScript($script, $charToClear){
    $keepGoing = $true
    while ($keepGoing)
    {
        if($script.Substring($script.Length - 1, 1) -eq $charToClear -or $script.Substring($script.Length - 1, 1) -eq " "){
            $script = $script.Substring(0, $script.Length - 1)
        } else {
            $keepGoing = $false
        }
    }

    return $script
}

Write-Host "Done"


#$x1 = GetSQLDataType -mainframeDataType "X(004)"
#$x2 = GetSQLDataType -mainframeDataType "9(002)"
#$x3 = GetSQLDataType -mainframeDataType "S9(008)"
#$x4 = GetSQLDataType -mainframeDataType "S9(008)V9(2)"


$xlCellTypeLastCell = 11 
$columnNameIndex = 2
$columnTypeIndex = 7
$columnMainframeTypeIndex = 0

$excel=new-object -com excel.application
$wb=$excel.workbooks.open("c:\users\gobm\Desktop\p2.xlsx")

for ($i=1; $i -le $wb.sheets.count - 1; $i++){
    $createScript = "CREATE TABLE"
    
    $sh = $wb.Sheets.Item($i)
    $tableName = $sh.name -replace " ", "_"

    if($tableName.Substring($tableName.Length - 1, 1) -eq "_"){
        $tableName = $tableName.Substring(0, $tableName.Length - 1)
    }

    $createScript = ("{0} {1} (" -f $createScript,$tableName)
    
    $maxRow=($sh.UsedRange.Rows).count
    $maxColumn=($sh.UsedRange.Columns).count

    for ($c=1;$c -le $maxColumn; $c++){
        $colName = $sh.Cells.Item(2,$c).Value2
        if($colName -eq "SQL DATA NAME"){
            $columnNameIndex = $c
        } elseif($colName -eq "Preferred  Datatype"){
            $columnTypeIndex = $c
        } elseif ($colName -eq "mainframe pic"){
            $columnMainframeTypeIndex = $c
        }
    }


    $createGetSP = "CREATE PROCEDURE"
    $createGetSP += (" spGet{0} `n" -f $tableName)
    $createGetSP += (" @startAt Datetime, `n")
    $createGetSP += (" @endAt Datetime `n")   
    $createGetSP += "AS `n"
    $createGetSP += "BEGIN `n"
    $createGetSP += "SELECT "

    for ($j=3;$j -le $maxRow; $j++){
        $nameValue = $sh.Cells.Item($j,$columnNameIndex).Value2

        if($columnMainframeTypeIndex -gt 0){
            $typeValue = GetSQLDataType -mainframeDataType $sh.Cells.Item($j,$columnTypeIndex).Value2
        } else {
            $typeValue = $sh.Cells.Item($j,$columnTypeIndex).Value2
        }
            
        if($nameValue){
            $nameValue = RemoveSpecialChars -word $nameValue
            $createScript = ("{0} {1} {2}," -f $createScript,$nameValue,$typeValue)
            #$createGetSP += ("`n`t`t {0}," -f $nameValue) 
            $createGetSP = buildSQLCreateProcedureStatement -isInitial $false -statement $createGetSP -valueToAdd $nameValue
        }
        
        Write-Host $j "/" $maxRow " >> " $i "/" $wb.sheets.count "(" $tableName ")"
    }

    
    $createScript = clearEndOfScript -script $createScript -charToClear "_"
    $createScript = clearEndOfScript -script $createScript -charToClear ","
    $creategetsp = clearEndOfScript -script $creategetsp -charToClear ","


    $createGetSP += ("`nFROM {0} `nEND " -f $tableName)

    $createScript = ("{0})" -f $createScript)

    $filePathToSave = ("{0}{1}{2}" -f "c:\users\gobm\Desktop\Scripts\", $tableName, ".sql")
    $filePathToSaveStoredProcedure = ("{0}{1}{2}" -f "c:\users\gobm\Desktop\Scripts\spGet", $tableName, ".sql")

    $createScript > $filePathToSave
    $createGetSP > $filePathToSaveStoredProcedure
}
$excel.Workbooks.Close() 
