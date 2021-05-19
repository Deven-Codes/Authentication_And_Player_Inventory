<?php

require 'ConnectionSettings.php';

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
  

//variables submitted by user
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];;

$sql = "SELECT username FROM users WHERE username = '" . $loginUser . "'";

$result = $conn->query($sql);

if($result->num_rows > 0) {
    //Tell user that name already exists
    echo "Username already exists.";
}
else 
{
    //Insert User and password into the database
    echo "Creating user......";
    $sql2 = "INSERT INTO users (username, password, level, coins) VALUES ('" . $loginUser . "', '" . $loginPass . "', '1', '0')";
    if($conn->query($sql2) == TRUE) {
        echo "New record created Successfully.";
    } else {
        echo "Error: " . $sql2 . "<br>" . $conn->error;
    }
}

$conn->close();

?>