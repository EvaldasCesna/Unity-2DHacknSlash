<?php
$Username = $_REQUEST["Username"];
$Email = $_REQUEST["Email"];
$Password = $_REQUEST["Password"];

$Hostname = "localhost";
$DBName = "accounts";
$User = "root";
$PasswordP = "";

mysql_connect($Hostname, $User, $PasswordP) or die("Cant Connct");
mysql_select_db($DBName) or die ("Cant conncet to db");

if(!$Email || !$Password || !$Username) {
    echo "Empty";
}
else
{
    $SQL = "SELECT * FROM accounts WHERE Email = '" . $Email . "'";
    $Result = @mysql_query($SQL) or die("Error");
    $Total = mysql_num_rows($Result);
    if($Total == 0){
        $insert = "INSERT INTO `accounts` (`Username`, `Email`, `Password`, `Characters`) VALUES ('" . $Username . "','" . $Email . "', MD5('" . $Password . "'), 0)";
        $SQL1 = mysql_query($insert);
        echo"Success";
    }
    else
    {
        echo"Exists";
    }
}


mysql_close();
?>