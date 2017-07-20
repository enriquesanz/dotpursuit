<?php

$ficheroOriginal = file_get_contents("trials-unity.txt");

$ficheroOriginal = explode("\n",$ficheroOriginal);

foreach ($ficheroOriginal as $linea)
{
    $linea = explode(" ",$linea);
    echo 'targetList.Add (new Target() {x = '.$linea[5].'f, y = '.$linea[7].'f, z = 0f, time = '.$linea[9].'f});'."\n";
    echo 'targetList.Add (new Target() {x = '.$linea[11].'f, y = '.$linea[13].'f, z = 0f, time = '.$linea[15].'f});'."\n";
    if ($linea[17] != 'NaN'){
        echo 'targetList.Add (new Target() {x = '.$linea[17].'f, y = '.$linea[19].'f, z = 0f, time = '.$linea[21].'f});'."\n";
    }
    if ($linea[23] != 'NaN'){
        echo 'targetList.Add (new Target() {x = '.$linea[23].'f, y = '.$linea[25].'f, z = 0f, time = '.$linea[27].'f});'."\n";
    }

    echo 'trialList.Add (new Trial () { nTrial = '.$linea[1].', trialType = '.$linea[3].', trialTargets = targetList});'."\n";
    echo 'targetList = new List<Target>();'."\n";

}


// targetList.Add (new Target() {x = -7.50f, y = -7f, z = 0f, time = 1f}); ***
// 		targetList.Add (new Target() {x = 5.50f, y = 0f, z = 0f, time = 198f}); ** 
// 		targetList.Add (new Target() {x = -1f, y = -1.45f, z = 0f, time = 758f}); **
// 		targetList.Add (new Target() {x = -7f, y = 6.50f, z = 0f, time = 794f}); **
// 		trialList.Add (new Trial () { nTrial = 1, trialType = 2, trialTargets = targetList});

// array(29) {
//   [0]=>
//   string(6) "nTrial"
//   [1]=>
//   string(3) "944"
//   [2]=>
//   string(9) "trialType"
//   [3]=>
//   string(1) "3"
//   [4]=>
//   string(2) "X1"
//   [5]=>
//   string(1) "0"
//   [6]=>
//   string(2) "Y1"
//   [7]=>
//   string(2) "-5"
//   [8]=>
//   string(2) "T1"
//   [9]=>
//   string(1) "1"
//   [10]=>
//   string(2) "X2"
//   [11]=>
//   string(2) "-8"
//   [12]=>
//   string(2) "Y2"
//   [13]=>
//   string(1) "0"
//   [14]=>
//   string(2) "T2"
//   [15]=>
//   string(3) "227"
//   [16]=>
//   string(2) "X3"
//   [17]=>
//   string(3) "NaN"
//   [18]=>
//   string(2) "Y3"
//   [19]=>
//   string(3) "NaN"
//   [20]=>
//   string(2) "T3"
//   [21]=>
//   string(4) "1166"
//   [22]=>
//   string(2) "X4"
//   [23]=>
//   string(3) "NaN"
//   [24]=>
//   string(2) "Y4"
//   [25]=>
//   string(3) "NaN"
//   [26]=>
//   string(2) "T4"
//   [27]=>
//   string(4) "1192"
//   [28]=>
// " string(1) "
// }

