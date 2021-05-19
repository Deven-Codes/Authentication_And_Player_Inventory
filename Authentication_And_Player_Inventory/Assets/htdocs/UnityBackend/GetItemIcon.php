<?php

    require 'ConnectionSettings.php';

    // Check connection
    if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
    }

    //variables submitted by user
    $itemID = $_POST["itemID"];

    $path = "http://localhost/UnityBackendTutorial/ItemsIcon/" . $itemID . ".png";

    // Get the image and convert into string
    $image = file_get_contents($path);

    echo $image;

    $conn->close();

?>