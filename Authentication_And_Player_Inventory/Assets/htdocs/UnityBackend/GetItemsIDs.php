<?php

require 'ConnectionSettings.php';

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

//User submitted variables
$userID = $_POST["userID"];

$sql = "SELECT ID, itemID FROM usersitems WHERE userID = '" . $userID . "'";

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