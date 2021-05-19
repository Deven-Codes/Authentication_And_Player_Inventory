<?php

require 'ConnectionSettings.php';

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}


$sql = "SELECT id, username, password, level, coins FROM users";

$result = $conn->query($sql);

if($result->num_rows > 0) {
    //output data of each row
    while($row = $result->fetch_assoc()){
        echo " Name : " . $row["username"] . "- Level : " . $row["level"] . "<br>";
    }
}
else 
{
    echo "0 results";
}

$conn->close();

?>