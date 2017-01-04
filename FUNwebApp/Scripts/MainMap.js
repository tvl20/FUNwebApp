$(function () {
    function Spel(config) {
        var canvas = document.getElementById("A");
        var ctx = canvas.getContext("2d");

        var defaults = {
            tileSize: 50,
            player: {
                AttackPoints: 0,
                MovePoints: 0,
                MovePointsPerMove: 1
            },
            currentRoom: {
                HumanEnemies: [
                    {
                        MovePoints: 0
                    }
                ],
                MonsterEnemies: [
                    {
                        MovePoints: 0
                    }
                ],
                BossEnemies: [
                    {
                        MovePoints: 0
                    }
                ]
            }
        };

        defaults = $.extend(true, defaults, config);

        //gets the current room
        $.get("/Map/GetRoom", function (data) {
            console.log(data);
            defaults.currentRoom = $.extend(true, defaults.currentRoom, data);
            //defaults.currentRoom = data;
            for (var i = 0; i < defaults.currentRoom.HumanEnemies.length; i++) {
                defaults.currentRoom.HumanEnemies[i].X = defaults.currentRoom.HumanEnemies[i].X * defaults.tileSize;
                defaults.currentRoom.HumanEnemies[i].Y = defaults.currentRoom.HumanEnemies[i].Y * defaults.tileSize;
            }
            for (var j = 0; j < defaults.currentRoom.MonsterEnemies.length; j++) {
                defaults.currentRoom.MonsterEnemies[j].X = defaults.currentRoom.MonsterEnemies[j].X * defaults.tileSize;
                defaults.currentRoom.MonsterEnemies[j].Y = defaults.currentRoom.MonsterEnemies[j].Y * defaults.tileSize;
            }
            for (var k = 0; k < defaults.currentRoom.BossEnemies.length; k++) {
                defaults.currentRoom.BossEnemies[k].X = defaults.currentRoom.BossEnemies[k].X * defaults.tileSize;
                defaults.currentRoom.BossEnemies[k].Y = defaults.currentRoom.BossEnemies[k].Y * defaults.tileSize;
            }
            render();
        });

        //gets the current player
        $.get("/Map/GetPlayer", function (data) {
            //console.log(data);
            defaults.player = $.extend(true, defaults.player, data);
            //defaults.player = data;
            defaults.player.X = defaults.player.X * defaults.tileSize;
            defaults.player.Y = defaults.player.Y * defaults.tileSize;
            render();
        });
        console.log(defaults);

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
            ctx.arc(defaults.player.X - defaults.tileSize / 2, defaults.player.Y - defaults.tileSize / 2, defaults.tileSize / 2, 0, 2 * Math.PI, false);
            ctx.fillStyle = "green";
            ctx.fill();
        }

        function drawEnemies() {
            for (var i = 0; i < defaults.currentRoom.HumanEnemies.length; i++) {
                ctx.beginPath();
                ctx.arc(defaults.currentRoom.HumanEnemies[i].X - defaults.tileSize / 2, defaults.currentRoom.HumanEnemies[i].Y - defaults.tileSize / 2, defaults.tileSize / 2, 0, 2 * Math.PI, false);
                ctx.fillStyle = "orange";
                ctx.fill();
            }
            for (var j = 0; j < defaults.currentRoom.MonsterEnemies.length; j++) {
                ctx.beginPath();
                ctx.arc(defaults.currentRoom.MonsterEnemies[j].X - defaults.tileSize / 2, defaults.currentRoom.MonsterEnemies[j].Y - defaults.tileSize / 2, defaults.tileSize / 2, 0, 2 * Math.PI, false);
                ctx.fillStyle = "red";
                ctx.fill();
            }
            for (var k = 0; k < defaults.currentRoom.BossEnemies.length; k++) {
                ctx.beginPath();
                ctx.arc(defaults.currentRoom.BossEnemies[k].X - defaults.tileSize / 2, defaults.currentRoom.BossEnemies[k].Y - defaults.tileSize / 2, defaults.tileSize / 2, 0, 2 * Math.PI, false);
                ctx.fillStyle = "grey";
                ctx.fill();
            }
        }

        function render() {
            if (tileA.complete && tileB.complete) {
                ctx.clearRect(0, 0, canvas.width, canvas.height);
                drawTileA();
                drawPlayer();
                drawEnemies();
            }
        }

        this.giveMovePoints = function() {
            if (defaults.player.MovePoints < defaults.player.MovePointsPerMove) {
                defaults.player.MovePoints++;
            }

            for (var i = 0; i < defaults.currentRoom.HumanEnemies.length; i++) {
                if (defaults.currentRoom.HumanEnemies[i].MovePoints < defaults.currentRoom.HumanEnemies[i].MovePointsPerMove) {
                    defaults.currentRoom.HumanEnemies[i].MovePoints++;
                    console.log("enemy " + i + " move points:" + defaults.currentRoom.HumanEnemies[i].MovePoints);
                }
            }
            for (var j = 0; j < defaults.currentRoom.MonsterEnemies.length; j++) {
                if (defaults.currentRoom.MonsterEnemies[j].MovePoints < defaults.currentRoom.MonsterEnemies[j].MovePointsPerMove) {
                    defaults.currentRoom.MonsterEnemies[j].MovePoints++;
                    console.log("enemy " + j + " move points:" + defaults.currentRoom.MonsterEnemies[j].MovePoints);
                }
            }
            for (var k = 0; k < defaults.currentRoom.BossEnemies.length; k++) {
                if (defaults.currentRoom.BossEnemies[k].MovePoints < defaults.currentRoom.BossEnemies[k].MovePointsPerMove) {
                    defaults.currentRoom.BossEnemies[k].MovePoints++;
                    console.log("enemy " + k + " move points:" + defaults.currentRoom.BossEnemies[k].MovePoints);
                }
            }
        }

        $(window).keyup(function (e) {
            if (defaults.player.MovePoints >= defaults.player.MovePointsPerMove) {
                //Speler positie aanpassen
                switch (e.keyCode) {
                case 38: //Up
                    defaults.player.Y -= defaults.tileSize;
                    break;
                case 39: //Right
                    defaults.player.X += defaults.tileSize;
                    break;
                case 37: //Left
                    defaults.player.X -= defaults.tileSize;
                    break;
                case 40: //Down
                    defaults.player.Y += defaults.tileSize;
                    break;
                }

                //Spel opnieuw renderen
                render();
                defaults.player.MovePoints = 0;
            }
        });


    }

    var game = new Spel({
        tileSize: 40
    });

    var timer = setInterval(myTimer, 250);
    function myTimer() {
        game.giveMovePoints();
    }

    var sound = document.getElementById("background_music");
    sound.loop = true;
    sound.play();
});