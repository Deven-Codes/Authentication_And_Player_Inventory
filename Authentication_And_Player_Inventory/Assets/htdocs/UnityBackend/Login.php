<?php

require 'ConnectionSettings.php';

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

//variables submitted by user
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];;

//Normal Query
// $sql = "SELECT password, id FROM users WHERE username = '" . $loginUser . "'";
// $result = $conn->query($sql);

// Prepared Statement
$sql = "SELECT password, id FROM users WHERE username = ?";
$statement = $conn->prepare($sql);
$statement->bind_param("s", $loginUser);
$statement->execute();
$result = $statement->get_result();

if($result->num_rows > 0) {
    //output data of each row
    while($row = $result->fetch_assoc()){
        if($row["password"] == $loginPass) {
            echo $row["id"]; // returning id of users table
        }
        else {
            echo "Wrong Credentials.";
        }
    }
}
else 
{
    echo "Username does not exists.";
}

$conn->close();

?>