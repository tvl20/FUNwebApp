﻿$(function () {
    function Spel(config) {
        var canvas = document.getElementById("A");
        var ctx = canvas.getContext("2d");

        var defaults = {
            tileSize: 50,
            player: {
                x: 0,
                y: 0
            }
        };

        defaults = $.extend(true, defaults, config);

        //sets the coordinates for (and gets) the enemies
        $.getJSON("/Map/Enemies", function (data) {
            for (var i = 0; i < data.length; i++) {
                console.log(data[i].Level);
            }
        });


        //set the coordinates for the player
        defaults.player.x = Math.round(canvas.width / 2 / defaults.tileSize) * defaults.tileSize;
        defaults.player.y = Math.round(canvas.height / 2 / defaults.tileSize) * defaults.tileSize;

        var tileA = new Image();
        tileA.src = "/Graphics/Tile.png";
        tileA.width = defaults.tileSize;
        tileA.height = defaults.tileSize;
        tileA.onload = render;

        var tileB = new Image();
        tileB.src = "/Graphics/Tile.png";
        tileB.width = defaults.tileSize;
        tileB.height = defaults.tileSize;
        tileB.onload = render;

        function drawTileA() {
            for (var x = 0; x < 30; x++) {
                for (var y = 0; y < 15; y++) {
                    ctx.drawImage(tileA, x * tileA.width, y * tileA.height);
                }
            }
        }

        function drawPlayer() {
            ctx.beginPath();
            ctx.arc(defaults.player.x - defaults.tileSize / 2, defaults.player.y - defaults.tileSize / 2, defaults.tileSize / 2, 0, 2 * Math.PI, false);
            ctx.fillStyle = "green";
            ctx.fill();
        }

        function render() {
            if (tileA.complete && tileB.complete) {
                ctx.clearRect(0, 0, canvas.width, canvas.height);
                drawTileA();
                drawPlayer();
            }
        }

        $(window)
            .keyup(function (e) {
                //Speler positie aanpassen
                switch (e.keyCode) {
                    case 38: //Up
                        defaults.player.y -= defaults.tileSize;
                        break;
                    case 39: //Right
                        defaults.player.x += defaults.tileSize;
                        break;
                    case 37: //Left
                        defaults.player.x -= defaults.tileSize;
                        break;
                    case 40: //Down
                        defaults.player.y += defaults.tileSize;
                        break;

                    default:
                }

                //Spel opnieuw renderen
                render();
            });

    }

    var game = new Spel({
        tileSize: 40
    });

    var enemyTimer = setInterval(myTimer, 10);
    function myTimer() {
        
    }

    var sound = document.getElementById("background_music");
    sound.loop = true;
    sound.play();
});