<?php
$Username = $_REQUEST["Username"];
$Password = $_REQUEST["Password"];

$Hostname = "localhost";
$DBName = "accounts";
$User = "root";
$PasswordP = "";

mysql_connect($Hostname, $User, $PasswordP) or die("Cant Connct");
mysql_select_db($DBName) or die ("Cant conncet to db");

if(!$Password || !$Username) {
    echo "Empty";
}
else
{
    $SQL = "SELECT * FROM accounts WHERE Username = '" . $Username . "'";
    $Result_id = @mysql_query($SQL) or die("Error");
    $Total = mysql_num_rows($Result_id);
    if($Total){
        $data = @mysql_fetch_array($Result_id);
        if(strcmp($Password,$data["Password"])) {
            $SQL2 = "SELECT Username FROM accounts WHERE Username = '" . $Username . "'";
            $Result_id2 = @mysql_query($SQL2) or die ("Error");
            while($row = mysql_fetch_array($Result_id2))
            {
                echo $row['Username'];
                echo":";
                echo"Success";
            }
        }
        else {
            echo"WrongPassword";
        }
    }
    else {
        echo"NameDoesNotExist";
    }
}


mysql_close();
?>