$(function () {
    function Spel(config) {
        var canvas = document.getElementById("A");
        var ctx = canvas.getContext("2d");

        var defaults = {
            tileSize: 50,
            roomWidth: 800,
            roomHeight: 500,
            player: {
                Name: "temp",
                Health: 1,
                MaxHealth: 1,
                AttackPoints: 0,
                AttackPointsPerAttack: 0,
                AttackPointsRegen: 0,
                MovePoints: 0,
                MovePointsPerMove: 1,
                Attack: 1,
                CurrentWeapon: {
                    WeaponDamage: 1
                }
            },
            currentRoom: {
                PlayerSpawnX: 1,
                PlayerSpawnY: 5,
                HumanEnemies: [
                    {
                        MovePoints: 0,
                        AttackPoints: 0,
                        Defence: 0
                    }
                ],
                MonsterEnemies: [
                    {
                        MovePoints: 0,
                        AttackPoints: 0,
                        Defence: 0
                    }
                ],
                BossEnemies: [
                    {
                        MovePoints: 0,
                        AttackPoints: 0,
                        Defence: 0
                    }
                ],
                WeaponOnGrounds: [
                    {
                        X: 0,
                        Y: 0
                    }
                ],
                Traps: [
                    {
                        Damage: 0,
                        X: 0,
                        Y: 0
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
            defaults.player.X = defaults.currentRoom.PlayerSpawnX * defaults.tileSize;
            defaults.player.Y = defaults.currentRoom.PlayerSpawnY * defaults.tileSize;
            $('#playername').val(defaults.player.Name);
            $('#hp').val(defaults.player.Health);
            $('#max_hp').val(defaults.player.MaxHealth);
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
            for (var x = 0; x < Math.round(defaults.roomWidth / defaults.tileSize) ; x++) {
                for (var y = 0; y < Math.round(defaults.roomHeight / defaults.tileSize) ; y++) {
                    ctx.drawImage(tileA, x * tileA.width, y * tileA.height);
                }
            }
        }

        this.updateInfo = function () {
            $('#playername').text(defaults.player.Name);
            $('#hp').text(defaults.player.Health);
            $('#max_hp').text(defaults.player.MaxHealth);
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

        this.giveMovePoints = function () {
            if (defaults.player.MovePoints < defaults.player.MovePointsPerMove) {
                defaults.player.MovePoints++;
            }

            for (var i = 0; i < defaults.currentRoom.HumanEnemies.length; i++) {
                if (defaults.currentRoom.HumanEnemies[i].MovePoints <
                    defaults.currentRoom.HumanEnemies[i].MovePointsPerMove) {
                    defaults.currentRoom.HumanEnemies[i].MovePoints++;
                    console.log("enemy " + i + " move points:" + defaults.currentRoom.HumanEnemies[i].MovePoints);
                }
            }
            for (var j = 0; j < defaults.currentRoom.MonsterEnemies.length; j++) {
                if (defaults.currentRoom.MonsterEnemies[j].MovePoints <
                    defaults.currentRoom.MonsterEnemies[j].MovePointsPerMove) {
                    defaults.currentRoom.MonsterEnemies[j].MovePoints++;
                    console.log("enemy " + j + " move points:" + defaults.currentRoom.MonsterEnemies[j].MovePoints);
                }
            }
            for (var k = 0; k < defaults.currentRoom.BossEnemies.length; k++) {
                if (defaults.currentRoom.BossEnemies[k].MovePoints <
                    defaults.currentRoom.BossEnemies[k].MovePointsPerMove) {
                    defaults.currentRoom.BossEnemies[k].MovePoints++;
                    console.log("enemy " + k + " move points:" + defaults.currentRoom.BossEnemies[k].MovePoints);
                }
            }
        }

        this.giveAttackPoints = function () {
            if (defaults.player.AttackPoints < defaults.player.AttackPointsPerAttack) {
                defaults.player.AttackPoints += defaults.player.AttackPointsRegen;
            }

            for (var i = 0; i < defaults.currentRoom.HumanEnemies.length; i++) {
                if (defaults.currentRoom.HumanEnemies[i].AttackPoints <
                    defaults.currentRoom.HumanEnemies[i].AttackPointsPerAttack) {
                    defaults.currentRoom.HumanEnemies[i].AttackPoints += defaults.currentRoom.HumanEnemies[i].AttackPointsRegen;
                    console.log("enemy " + i + " attack points:" + defaults.currentRoom.HumanEnemies[i].MovePoints);
                }
            }
            for (var j = 0; j < defaults.currentRoom.MonsterEnemies.length; j++) {
                if (defaults.currentRoom.MonsterEnemies[j].AttackPoints <
                    defaults.currentRoom.MonsterEnemies[j].AttackPointsPerAttack) {
                    defaults.currentRoom.MonsterEnemies[j].AttackPoints += defaults.currentRoom.MonsterEnemies[j].AttackPointsRegen;
                    console.log("enemy " + j + " attack points:" + defaults.currentRoom.MonsterEnemies[j].MovePoints);
                }
            }
            for (var k = 0; k < defaults.currentRoom.BossEnemies.length; k++) {
                if (defaults.currentRoom.BossEnemies[k].AttackPoints <
                    defaults.currentRoom.BossEnemies[k].AttackPointsPerAttack) {
                    defaults.currentRoom.BossEnemies[k].AttackPoints += defaults.currentRoom.BossEnemies[k].AttackPointsRegen;
                    console.log("enemy " + k + " attack points:" + defaults.currentRoom.BossEnemies[k].MovePoints);
                }
            }
        }

        //enemy movement
        this.actionEnemy = function () {
            var pX = defaults.player.X;
            var pY = defaults.player.Y;
            var eX;   // this variables exist for readability
            var eY;   // this variables exist for readability

            //for all the monster enemies
            for (var i = 0; i < defaults.currentRoom.MonsterEnemies.length; i++) {
                eX = defaults.currentRoom.MonsterEnemies[i].X;
                eY = defaults.currentRoom.MonsterEnemies[i].Y;
                if (
                    ((pX === eX) && (pY === (eY - defaults.tileSize))) || //player is above the enemy
                        ((pX === (eX + defaults.tileSize)) && (pY === eY)) || //player is right of the enemy
                        ((pX === eX) && (pY === (eY + defaults.tileSize))) || //player is under the enemy
                        ((pX === (eX - defaults.tileSize)) && (pY === eY)) //player is left of the enemy
                ) {
                    //attack player
                } else {
                    switch (getEnemyMoveDir(eX, eY)) {
                        case "N":
                            moveEnemy(eX, eY - defaults.tileSize, 0, i);
                            break;
                        case "S":
                            moveEnemy(eX, eY + defaults.tileSize, 0, i);
                            break;
                        case "E":
                            moveEnemy(eX + defaults.tileSize, eY, 0, i);
                            break;
                        case "W":
                            moveEnemy(eX - defaults.tileSize, eY, 0, i);
                            break;
                    }
                }
            }

            //for all the human enemies
            for (var i = 0; i < defaults.currentRoom.HumanEnemies.length; i++) {
                eX = defaults.currentRoom.HumanEnemies[i].X;
                eY = defaults.currentRoom.HumanEnemies[i].Y;
                if (
                    ((pX === eX) && (pY === (eY - defaults.tileSize))) || //player is above the enemy
                        ((pX === (eX + defaults.tileSize)) && (pY === eY)) || //player is right of the enemy
                        ((pX === eX) && (pY === (eY + defaults.tileSize))) || //player is under the enemy
                        ((pX === (eX - defaults.tileSize)) && (pY === eY)) //player is left of the enemy
                ) {
                    //attack player
                } else {
                    switch (getEnemyMoveDir(eX, eY)) {
                        case "N":
                            moveEnemy(eX, eY - defaults.tileSize, 1, i);
                            break;
                        case "S":
                            moveEnemy(eX, eY + defaults.tileSize, 1, i);
                            break;
                        case "E":
                            moveEnemy(eX + defaults.tileSize, eY, 1, i);
                            break;
                        case "W":
                            moveEnemy(eX - defaults.tileSize, eY, 1, i);
                            break;
                    }
                }
            }

            //for all the boss enemies
            for (var i = 0; i < defaults.currentRoom.BossEnemies.length; i++) {
                eX = defaults.currentRoom.BossEnemies[i].X;
                eY = defaults.currentRoom.BossEnemies[i].Y;
                if (
                    ((pX === eX) && (pY === (eY - defaults.tileSize))) || //player is above the enemy
                        ((pX === (eX + defaults.tileSize)) && (pY === eY)) || //player is right of the enemy
                        ((pX === eX) && (pY === (eY + defaults.tileSize))) || //player is under the enemy
                        ((pX === (eX - defaults.tileSize)) && (pY === eY)) //player is left of the enemy
                ) {
                    //attack player
                } else {
                    switch (getEnemyMoveDir(eX, eY)) {
                        case "N":
                            moveEnemy(eX, eY - defaults.tileSize, 2, i);
                            break;
                        case "S":
                            moveEnemy(eX, eY + defaults.tileSize, 2, i);
                            break;
                        case "E":
                            moveEnemy(eX + defaults.tileSize, eY, 2, i);
                            break;
                        case "W":
                            moveEnemy(eX - defaults.tileSize, eY, 2, i);
                            break;
                    }
                }
            }
        }

        function getEnemyMoveDir(x, y) {//possible outcomes: "N" North, "E" East, "S" South, "W" West
            //enemy moves in the first direction it finds (Priority: Up, Down, Right, Left)
            if (defaults.player.Y < y) { return "N" }
            if (defaults.player.Y > y) { return "S" }
            if (defaults.player.X < x) { return "W" }
            if (defaults.player.X > x) { return "E" }
            return ""; //default
        }

        function moveEnemy(x, y, soort, index) { //0 = monster, 1 = human, 2 = boss
            var occ = getOccupation(x, y);
            if ((occ === "trap") || (occ === "empty")) {
                switch (soort) {
                    case 0:
                        if (defaults.currentRoom.MonsterEnemies[index].MovePoints >= defaults.currentRoom.MonsterEnemies[index].MovePointsPerMove) {
                            defaults.currentRoom.MonsterEnemies[index].X = x;
                            defaults.currentRoom.MonsterEnemies[index].Y = y;
                            defaults.currentRoom.MonsterEnemies[index].MovePoints -= defaults.currentRoom.MonsterEnemies[index].MovePointsPerMove;
                        }
                        break;
                    case 1:
                        if (defaults.currentRoom.HumanEnemies[index].MovePoints >= defaults.currentRoom.HumanEnemies[index].MovePointsPerMove) {
                            defaults.currentRoom.HumanEnemies[index].X = x;
                            defaults.currentRoom.HumanEnemies[index].Y = y;
                            defaults.currentRoom.HumanEnemies[index].MovePoints -= defaults.currentRoom.HumanEnemies[index].MovePointsPerMove;
                        }
                        break;
                    case 2:
                        if (defaults.currentRoom.BossEnemies[index].MovePoints >= defaults.currentRoom.BossEnemies[index].MovePointsPerMove) {
                            defaults.currentRoom.BossEnemies[index].X = x;
                            defaults.currentRoom.BossEnemies[index].Y = y;
                            defaults.currentRoom.BossEnemies[index].MovePoints -= defaults.currentRoom.BossEnemies[index].MovePointsPerMove;
                        }
                        break;
                }
            }
            render();
        }

        //attacking an enemy
        function attackEnemy(enemyX, enemyY) {
            if (defaults.player.AttackPoints >= defaults.player.AttackPointsPerAttack) {
                defaults.player.AttackPoints -= defaults.player.AttackPointsPerAttack;
                for (var i = 0; i < defaults.currentRoom.MonsterEnemies.length; i++) {
                    if ((defaults.currentRoom.MonsterEnemies[i].X === enemyX) &&
                        (defaults.currentRoom.MonsterEnemies[i].Y === enemyY)) {
                        defaults.currentRoom.MonsterEnemies[i].Health -=
                            ((defaults.player.Attack + defaults.player.CurrentWeapon.WeaponDamage) - defaults.currentRoom.MonsterEnemies[i].Defence);
                        if (defaults.currentRoom.MonsterEnemies[i].Health <= 0) {
                            defaults.currentRoom.MonsterEnemies.splice(i, 1);
                            i--;
                            render();
                        }
                    }
                }
                for (var j = 0; j < defaults.currentRoom.HumanEnemies.length; j++) {
                    if ((defaults.currentRoom.HumanEnemies[j].X === enemyX) &&
                        (defaults.currentRoom.HumanEnemies[j].Y === enemyY)) {
                        defaults.currentRoom.HumanEnemies[j].Health -=
                        ((defaults.player.Attack + defaults.player.CurrentWeapon.WeaponDamage) - defaults.currentRoom.HumanEnemies[j].Defence);
                        if (defaults.currentRoom.HumanEnemies[j].Health <= 0) {
                            defaults.currentRoom.HumanEnemies.splice(j, 1);
                            j--;
                            render();
                        }
                    }
                }
                for (var k = 0; k < defaults.currentRoom.BossEnemies.length; k++) {
                    if ((defaults.currentRoom.BossEnemies[k].X === enemyX) &&
                        (defaults.currentRoom.BossEnemies[k].Y === enemyY)) {
                        defaults.currentRoom.BossEnemies[k].Health -=
                        ((defaults.player.Attack + defaults.player.CurrentWeapon.WeaponDamage) - defaults.currentRoom.BossEnemies[k].Defence);
                        if (defaults.currentRoom.BossEnemies[k].Health <= 0) {
                            defaults.currentRoom.BossEnemies.splice(j, 1);
                            k--;
                            render();
                        }
                    }
                }
            }
        }

        //check what is on the position {enemy, weapon, trap, empty}
        function getOccupation(x, y) {
            var completed = false;
            for (var i = 0; i < defaults.currentRoom.MonsterEnemies.length; i++) {
                if ((defaults.currentRoom.MonsterEnemies[i].X === x) &&
                    (defaults.currentRoom.MonsterEnemies[i].Y === y)) {
                    return "enemy";
                }
            }
            for (var j = 0; j < defaults.currentRoom.HumanEnemies.length; j++) {
                if ((defaults.currentRoom.HumanEnemies[j].X === x) &&
                    (defaults.currentRoom.HumanEnemies[j].Y === y)) {
                    return "enemy";
                }
            }
            for (var k = 0; k < defaults.currentRoom.BossEnemies.length; k++) {
                if ((defaults.currentRoom.BossEnemies[k].X === x) &&
                    (defaults.currentRoom.BossEnemies[k].Y === y)) {
                    return "enemy";
                }
            }

            for (var l = 0; l < defaults.currentRoom.WeaponOnGrounds; l++) {
                if ((defaults.currentRoom.WeaponOnGrounds[l].X === x) &&
                    (defaults.currentRoom.WeaponOnGrounds[l].Y === y)) {
                    return "weapon";
                }
            }

            for (var m = 0; m < defaults.currentRoom.Traps; m++) {
                if ((defaults.currentRoom.Traps[m].X === x) &&
                    (defaults.currentRoom.Traps[m].Y === y)) {
                    return "trap";
                }
            }

            return "empty";
        }


        //Speler positie aanpassen
        $(window).keyup(function (e) {
            if (defaults.player.MovePoints >= defaults.player.MovePointsPerMove) {
                var pos;
                var occ;
                switch (e.keyCode) {
                    case 38: //Up
                        pos = defaults.player.Y - defaults.tileSize;
                        occ = getOccupation(defaults.player.X, pos);
                        if (pos > 0) {
                            if ((occ === "trap") || (occ === "empty")) {
                                defaults.player.Y -= defaults.tileSize;
                                defaults.player.MovePoints = 0;
                            } else {
                                attackEnemy(defaults.player.X, pos);
                            }
                        }
                        break;
                    case 39: //Right
                        pos = defaults.player.X + defaults.tileSize;
                        occ = getOccupation(pos, defaults.player.Y);
                        if (pos <= defaults.roomWidth) {
                            if ((occ === "trap") || (occ === "empty")) {
                                defaults.player.X += defaults.tileSize;
                                defaults.player.MovePoints = 0;
                            } else {
                                attackEnemy(pos, defaults.player.Y);
                            }
                        }
                        break;
                    case 37: //Left
                        pos = defaults.player.X - defaults.tileSize;
                        occ = getOccupation(pos, defaults.player.Y);
                        if (pos > 0) {
                            if ((occ === "trap") || (occ === "empty")) {
                                defaults.player.X -= defaults.tileSize;
                                defaults.player.MovePoints = 0;
                            } else {
                                attackEnemy(pos, defaults.player.Y);
                            }
                        }
                        break;
                    case 40: //Down
                        pos = defaults.player.Y + defaults.tileSize;
                        occ = getOccupation(defaults.player.X, pos);
                        if (pos <= defaults.roomHeight) {
                            if ((occ === "trap") || (occ === "empty")) {
                                defaults.player.Y += defaults.tileSize;
                                defaults.player.MovePoints = 0;
                            } else {
                                attackEnemy(defaults.player.X, pos);
                            }
                        }
                        break;
                }

                //Spel opnieuw renderen
                render();
            }
        });


    }

    var game = new Spel({
        tileSize: 50
    });

    var timer = setInterval(myTimer, 250);
    function myTimer() {
        game.giveMovePoints();
        game.giveAttackPoints();
        game.actionEnemy();
        game.updateInfo();
    }

    var sound = document.getElementById("background_music");
    sound.loop = true;
    sound.play();
});