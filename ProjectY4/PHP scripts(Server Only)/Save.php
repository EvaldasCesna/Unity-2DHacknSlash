<?php
$Username = $_REQUEST["Username"];
$Inventory = $_REQUEST["Inventory"];
$Equipment = $_REQUEST["Equipment"];
$Gold = $_REQUEST["Gold"];
$Level = $_REQUEST["Level"];
$Xp = $_REQUEST["Xp"];
$Mobs = $_REQUEST["Mobs"];
$Bosses = $_REQUEST["Bosses"];

$Hostname = "localhost";
$DBName = "accounts";
$User = "root";
$PasswordP = "";

mysql_connect($Hostname, $User, $PasswordP) or die("Cant Connect");
mysql_select_db($DBName) or die ("Cant connect to DB");

if(!$Inventory || !$Username) {
    echo "Empty";
}
else
{
    $SQL = "SELECT * FROM accounts WHERE Username = '" . $Username . "'";
    $Result = @mysql_query($SQL) or die("Error");
    $Total = mysql_num_rows($Result);
    if($Total == 1){
        $insert = "UPDATE `accounts` SET Inventory = '" . $Inventory . "', Equipment = '" . $Equipment . 
        "', Gold = '" . $Gold . "', Level = '" . $Level . "', Xp = '" . $Xp . "', Mobs = '" . $Mobs . "', Bosses = '" . $Bosses . "
        ' WHERE Username = '" . $Username ."'";
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