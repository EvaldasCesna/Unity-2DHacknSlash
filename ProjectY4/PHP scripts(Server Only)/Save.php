<?php
$Username = $_REQUEST["Username"];
$Inventory = $_REQUEST["Inventory"];


$Hostname = "localhost";
$DBName = "accounts";
$User = "root";
$PasswordP = "";

mysql_connect($Hostname, $User, $PasswordP) or die("Cant Connct");
mysql_select_db($DBName) or die ("Cant conncet to db");

if(!$Inventory || !$Username) {
    echo "Empty";
}
else
{
    $SQL = "SELECT * FROM accounts WHERE Username = '" . $Username . "'";
    $Result = @mysql_query($SQL) or die("Error");
    $Total = mysql_num_rows($Result);
    if($Total == 1){
        $insert = "UPDATE `accounts` SET Inventory = '" . $Inventory . "' WHERE Username = '" . $Username ."'";
        $SQL1 = mysql_query($insert);
        echo"Success";
    }
    else
    {
        echo"Doesnt Exist";
    }
}


mysql_close();
?>