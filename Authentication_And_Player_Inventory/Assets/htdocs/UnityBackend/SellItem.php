<?php

require 'ConnectionSettings.php';

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

//Variables submitted by user
$id = $_POST["ID"];
$itemID = $_POST["itemID"];
$userID = $_POST["userID"];

//First SQL
$sql = "SELECT price FROM items WHERE id = '" . $itemID . "'";

$result = $conn->query($sql);

if($result->num_rows > 0) {
    //Store item price
    $itemPrice = $result->fetch_assoc()["price"];

    //Second SQL(delete item)
    $sql2 = "DELETE FROM usersitems WHERE ID = '" . $id .  "'";

    $result2 = $conn->query($sql2);
    if($result2) {
        //If deleted Successfully
        $sql3 = "UPDATE users SET coins = coins + '" . $itemPrice . "' WHERE id = '" . $userID . "'";
        $conn->query($sql3);
    } else {
        echo "error: could not delete an item";
    }
}
else 
{
    echo "0";
}

$conn->close();

?>