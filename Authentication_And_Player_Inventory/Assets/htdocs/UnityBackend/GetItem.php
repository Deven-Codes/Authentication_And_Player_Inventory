<?php

require 'ConnectionSettings.php';

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

//variables submitted by user
$itemID = $_POST["itemID"];



$sql = "SELECT name, description, price, imgVer FROM items WHERE id = '" . $itemID . "'";

$result = $conn->query($sql);

if($result->num_rows > 0) {
    //output data of each row
    $rows = array();
    while($row = $result->fetch_assoc()){
        $rows[] = $row;
    }
    echo json_encode($rows);
}
else 
{
    echo "0";
}

$conn->close();

?>