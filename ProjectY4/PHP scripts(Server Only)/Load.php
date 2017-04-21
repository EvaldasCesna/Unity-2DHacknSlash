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
    
        $SQL2 = "SELECT Inventory FROM accounts WHERE Username = '" . $Username . "'";
        $SQL3 = "SELECT Equipment FROM accounts WHERE Username = '" . $Username . "'";
        $SQL4 = "SELECT Gold FROM accounts WHERE Username = '" . $Username . "'";
        $SQL5 = "SELECT Level FROM accounts WHERE Username = '" . $Username . "'";
        $SQL6 = "SELECT Xp FROM accounts WHERE Username = '" . $Username . "'";
        $SQL7 = "SELECT Mobs FROM accounts WHERE Username = '" . $Username . "'";
        $SQL8 = "SELECT Bosses FROM accounts WHERE Username = '" . $Username . "'";

        $Result_id2 = @mysql_query($SQL2) or die ("Error");
        while($row = mysql_fetch_array($Result_id2))
        {
            echo $row['Inventory'];
        }
            echo"*";
        $Result_id3 = @mysql_query($SQL3) or die ("Error");
        while($row = mysql_fetch_array($Result_id3))
        {  
            echo $row['Equipment'];
        }
             echo"*";
        $Result_id4 = @mysql_query($SQL4) or die ("Error");
        while($row = mysql_fetch_array($Result_id4))
        {  
            echo $row['Gold'];
        }
             echo"*";
        $Result_id5 = @mysql_query($SQL5) or die ("Error");
        while($row = mysql_fetch_array($Result_id5))
        {  
            echo $row['Level'];
        }
             echo"*";
        $Result_id6 = @mysql_query($SQL6) or die ("Error");
        while($row = mysql_fetch_array($Result_id6))
        {  
            echo $row['Xp'];
        }
             echo"*";
        $Result_id7 = @mysql_query($SQL7) or die ("Error");
        while($row = mysql_fetch_array($Result_id7))
        {  
            echo $row['Mobs'];
        }
             echo"*";
        $Result_id8 = @mysql_query($SQL8) or die ("Error");
        while($row = mysql_fetch_array($Result_id8))
        {  
            echo $row['Bosses'];
        }
    }
    else
    {
        echo"NameDoesNotExist";
    }
}


mysql_close();
?>