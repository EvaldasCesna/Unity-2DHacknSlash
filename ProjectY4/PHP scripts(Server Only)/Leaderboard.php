<?php
$Username = $_REQUEST["Username"];

$Hostname = "localhost";
$DBName = "accounts";
$User = "root";
$PasswordP = "";

mysql_connect($Hostname, $User, $PasswordP) or die("Cant Connect");
mysql_select_db($DBName) or die ("Cant connect to DB");

if(!$Username) {
    echo "Empty";
}
else
{
    $SQL = "SELECT * FROM accounts WHERE Username = '" . $Username . "'";
    $Result_id = @mysql_query($SQL) or die("Error");
    $Total = mysql_num_rows($Result_id);
    if($Total)
    {
        $data = @mysql_fetch_array($Result_id);
    
        $SQL2 = "SELECT Username FROM accounts ORDER BY Mobs DESC";
        $SQL3 = "SELECT Mobs FROM accounts ORDER BY Mobs DESC";
        $SQL4 = "SELECT Bosses FROM accounts ORDER BY Mobs DESC";

        $Result_id2 = @mysql_query($SQL2) or die ("Error");
        while($row = mysql_fetch_array($Result_id2))
        {
           
            echo $row['Username'];
            echo "\t";
        }
            echo"*";
        $Result_id3 = @mysql_query($SQL3) or die ("Error");
        while($row = mysql_fetch_array($Result_id3))
        {  
            echo $row['Mobs'];
             echo "\t   ";
        }
             echo"*";
        $Result_id4 = @mysql_query($SQL4) or die ("Error");
        while($row = mysql_fetch_array($Result_id4))
        {  
            echo $row['Bosses'];
             echo "\t   ";
        }
    }
    else
    {
        echo"NameDoesNotExist";
    }
}


mysql_close();
?>