$(function () {
    function Spel(config) {
        var canvas = document.getElementById("A");
        var ctx = canvas.getContext("2d");
        var regenCounter = 0;

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
                    WeaponDamage: 1,
                    WeaponCrit: 0
                },
                XP: 0,
                Level: 0
            },
            currentRoom: {
                Clear: false,
                LocationID: 0,
                PreviousRoomID: 0,
                RoomID: 0,
                NextRoomID: 0,
                PlayerSpawnX: 2,
                PlayerSpawnY: 2,
                HumanEnemies: [{}],
                MonsterEnemies: [{}],
                BossEnemies: [{}],
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
            console.log("Data");
            console.log(data);
            defaults.currentRoom = $.extend(true, defaults.currentRoom, data);
            //defaults.currentRoom = data;
            if (data.HumanEnemies.length === 0){defaults.currentRoom.HumanEnemies = [];}
            if (data.MonsterEnemies.length === 0) { defaults.currentRoom.MonsterEnemies = []; }
            if (data.BossEnemies.length === 0) { defaults.currentRoom.BossEnemies = []; }
            
            if (data.Traps.length === 0) { defaults.currentRoom.Traps = []; }
            if (data.WeaponOnGrounds.length === 0) { defaults.currentRoom.WeaponOnGrounds = []; }

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

            for (var l = 0; l < defaults.currentRoom.Traps.length; l++) {
                defaults.currentRoom.Traps[l].X = defaults.currentRoom.Traps[l].X * defaults.tileSize;
                defaults.currentRoom.Traps[l].Y = defaults.currentRoom.Traps[l].Y * defaults.tileSize;
            }
            for (var m = 0; m < defaults.currentRoom.WeaponOnGrounds.length; m++) {
                defaults.currentRoom.WeaponOnGrounds[m].X = defaults.currentRoom.WeaponOnGrounds[m].X * defaults.tileSize;
                defaults.currentRoom.WeaponOnGrounds[m].Y = defaults.currentRoom.WeaponOnGrounds[m].Y * defaults.tileSize;
            }
            $.post("/Map/GetPreviousRoomID", { currentRoomID: defaults.currentRoom.RoomID }, function (data) {
                defaults.currentRoom.PreviousRoomID = data;
            });
            render();
            console.log("Def");
            console.log(defaults);
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
        //console.log(defaults);

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
            for (var x = 0; x < Math.round(defaults.roomWidth / defaults.tileSize); x++) {
                for (var y = 0; y < Math.round(defaults.roomHeight / defaults.tileSize); y++) {
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

        function drawOpenTraps() {
            for (var i = 0; i < defaults.currentRoom.Traps.length; i++) {
                if (defaults.currentRoom.Traps[i].Damage === 0) {
                    ctx.beginPath();
                    ctx.arc(defaults.currentRoom.Traps[i].X - defaults.tileSize / 2, defaults.currentRoom.Traps[i].Y - defaults.tileSize / 2, defaults.tileSize / 2, 0, 2 * Math.PI, false);
                    ctx.fillStyle = "grey";
                    ctx.fill();
                }
            }
        }

        function drawWeaponsOnTheGround() {
            for (var i = 0; i < defaults.currentRoom.WeaponOnGrounds.length; i++) {
                ctx.beginPath();
                ctx.arc(defaults.currentRoom.WeaponOnGrounds[i].X - defaults.tileSize / 2, defaults.currentRoom.WeaponOnGrounds[i].Y - defaults.tileSize / 2, defaults.tileSize / 2, 0, 2 * Math.PI, false);
                ctx.fillStyle = "blue";
                ctx.fill();
            }
        }

        function drawDoors() {
            defaults.currentRoom.Clear = true;
            //left door
            if (defaults.currentRoom.RoomID !== defaults.currentRoom.PreviousRoomID) {
                ctx.beginPath();
                ctx.rect(0, 0, defaults.tileSize, defaults.roomHeight);
                ctx.fillStyle = "brown";
                ctx.fill();
            }

            //right door
            if (defaults.currentRoom.RoomID !== defaults.currentRoom.NextRoomID) {
                ctx.beginPath();
                ctx.rect(defaults.roomWidth - defaults.tileSize, 0, defaults.roomWidth, defaults.roomHeight);
                ctx.fillStyle = "brown";
                ctx.fill();
            }
        }

        function render() { //draw the room on the canvas element
            if (tileA.complete && tileB.complete) {
                ctx.clearRect(0, 0, canvas.width, canvas.height);
                drawTileA();
                if (defaults.currentRoom.MonsterEnemies.length === 0 &&
                    defaults.currentRoom.HumanEnemies.length === 0 &&
                    defaults.currentRoom.BossEnemies.length === 0) {
                    drawDoors();
                }
                drawOpenTraps();
                drawWeaponsOnTheGround();
                drawPlayer();
                drawEnemies();
            }
        }

        this.updateInfo = function () {
            $('#location').text("Location: " + defaults.currentRoom.LocationID + ", CurrentRoomID: " + defaults.currentRoom.RoomID);
            $('#playername').text(defaults.player.Name);
            $('#currentLvl').text(defaults.player.Level);
            $('#xp').text(defaults.player.XP);
            $('#targetxp').text(defaults.player.Level * (defaults.player.Level + 5));
            $('#hp').text(defaults.player.Health);
            $('#max_hp').text(defaults.player.MaxHealth);
            $('#mp').text(defaults.player.MovePoints);
            $('#max_mp').text(defaults.player.MovePointsPerMove);
            $('#ap').text(defaults.player.AttackPoints);
            $('#max_ap').text(defaults.player.AttackPointsPerAttack);
            $('#weaponname').text(defaults.player.CurrentWeapon.WeaponName);
            $('#weapondamage').text(defaults.player.CurrentWeapon.WeaponDamage);
            $('#weaponcrit').text(defaults.player.CurrentWeapon.WeaponCrit);
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

        this.playerRegen = function () {
            if (defaults.player.Health < defaults.player.MaxHealth) {
                if (regenCounter === 6) {
                    regenCounter = 0;
                    defaults.player.Health += (Math.round(defaults.player.Health / 100) + 1);
                    $.post('/Map/UpdatePlayerHealth', { hp: defaults.player.Health });
                } else {
                    regenCounter++;
                }
            }
        }

        //make an enemy do something (attacking the player or moving)
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
                    console.log(defaults.currentRoom.MonsterEnemies[i]);
                    if (defaults.currentRoom.MonsterEnemies[i].AttackPoints >= defaults.currentRoom.MonsterEnemies[i].AttackPointsPerAttack) {
                        defaults.currentRoom.MonsterEnemies[i].AttackPoints -= defaults.currentRoom.MonsterEnemies[i].AttackPointsPerAttack;
                        defaults.player.Health -= defaults.currentRoom.MonsterEnemies[i].TrueDamage;
                        if (defaults.player.Defence < defaults.currentRoom.MonsterEnemies[i].Attack){
                            defaults.player.Health -= (defaults.currentRoom.MonsterEnemies[i].Attack - defaults.player.Defence);
                        }
                        $.post('/Map/UpdatePlayerHealth', { hp: defaults.player.Health });
                    }
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
            for (var j = 0; j < defaults.currentRoom.HumanEnemies.length; j++) {
                eX = defaults.currentRoom.HumanEnemies[j].X;
                eY = defaults.currentRoom.HumanEnemies[j].Y;
                if (
                    ((pX === eX) && (pY === (eY - defaults.tileSize))) || //player is above the enemy
                        ((pX === (eX + defaults.tileSize)) && (pY === eY)) || //player is right of the enemy
                        ((pX === eX) && (pY === (eY + defaults.tileSize))) || //player is under the enemy
                        ((pX === (eX - defaults.tileSize)) && (pY === eY)) //player is left of the enemy
                ) {
                    if (defaults.currentRoom.HumanEnemies[j].AttackPoints >= defaults.currentRoom.HumanEnemies[j].AttackPointsPerAttack) {
                        defaults.currentRoom.HumanEnemies[j].AttackPoints -= defaults.currentRoom.HumanEnemies[j].AttackPointsPerAttack;
                        var rnd = Math.floor((Math.random() * 101));
                        var dmg = defaults.currentRoom.HumanEnemies[j].Attack;
                        if (rnd >= defaults.currentRoom.HumanEnemies[j].CritChance) {
                            dmg = Math.round(dmg * 1.30);
                        }
                        if (defaults.player.Defence < dmg) {
                            defaults.player.Health -= (dmg - defaults.player.Defence);
                            $.post('/Map/UpdatePlayerHealth', { hp: defaults.player.Health });
                        }
                    }
                } else {
                    switch (getEnemyMoveDir(eX, eY)) {
                        case "N":
                            moveEnemy(eX, eY - defaults.tileSize, 1, j);
                            break;
                        case "S":
                            moveEnemy(eX, eY + defaults.tileSize, 1, j);
                            break;
                        case "E":
                            moveEnemy(eX + defaults.tileSize, eY, 1, j);
                            break;
                        case "W":
                            moveEnemy(eX - defaults.tileSize, eY, 1, j);
                            break;
                    }
                }
            }

            //for all the boss enemies
            for (var k = 0; k < defaults.currentRoom.BossEnemies.length; k++) {
                eX = defaults.currentRoom.BossEnemies[k].X;
                eY = defaults.currentRoom.BossEnemies[k].Y;
                if (
                    ((pX === eX) && (pY === (eY - defaults.tileSize))) || //player is above the enemy
                        ((pX === (eX + defaults.tileSize)) && (pY === eY)) || //player is right of the enemy
                        ((pX === eX) && (pY === (eY + defaults.tileSize))) || //player is under the enemy
                        ((pX === (eX - defaults.tileSize)) && (pY === eY)) //player is left of the enemy
                ) {
                    if (defaults.currentRoom.BossEnemies[k].AttackPoints >= defaults.currentRoom.BossEnemies[k].AttackPointsPerAttack) {
                        defaults.currentRoom.BossEnemies[k].AttackPoints -= defaults.currentRoom.BossEnemies[k].AttackPointsPerAttack;
                        if (defaults.player.Defence > defaults.currentRoom.BossEnemies[k].Attack) {
                            defaults.player.Health -= (defaults.currentRoom.BossEnemies[k].Attack - defaults.player.Defence);
                            $.post('/Map/UpdatePlayerHealth', { hp: defaults.player.Health });
                        }
                    }
                } else {
                    switch (getEnemyMoveDir(eX, eY)) {
                        case "N":
                            moveEnemy(eX, eY - defaults.tileSize, 2, k);
                            break;
                        case "S":
                            moveEnemy(eX, eY + defaults.tileSize, 2, k);
                            break;
                        case "E":
                            moveEnemy(eX + defaults.tileSize, eY, 2, k);
                            break;
                        case "W":
                            moveEnemy(eX - defaults.tileSize, eY, 2, k);
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
            return ""; //default (theoretically it should never reach this)
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
                
                var damage;
                var rnd;
                for (var i = 0; i < defaults.currentRoom.MonsterEnemies.length; i++) {
                    if ((defaults.currentRoom.MonsterEnemies[i].X === enemyX) &&
                        (defaults.currentRoom.MonsterEnemies[i].Y === enemyY)) {
                        rnd = Math.floor((Math.random() * 101));
                        damage = defaults.player.Attack + defaults.player.CurrentWeapon.WeaponDamage;
                        if (rnd < defaults.player.CurrentWeapon.WeaponCrit) {
                            damage = damage * 1.30;
                        }

                        if (damage > defaults.currentRoom.MonsterEnemies[i].Defence) {
                            damage -= defaults.currentRoom.MonsterEnemies[i].Defence;
                            defaults.currentRoom.MonsterEnemies[i].Health -= damage;

                            //give the player XP for the damage inflicted
                            $.post('/Map/AddPlayerXP', { xp: damage });
                            defaults.player.XP += damage;
                            if (defaults.currentRoom.MonsterEnemies[i].Health <= 0) {
                                defaults.currentRoom.MonsterEnemies.splice(i, 1);
                                i--;
                                render();
                            }
                        }
                    }
                }
                for (var j = 0; j < defaults.currentRoom.HumanEnemies.length; j++) {
                    if ((defaults.currentRoom.HumanEnemies[j].X === enemyX) &&
                        (defaults.currentRoom.HumanEnemies[j].Y === enemyY)) {
                        rnd = Math.floor((Math.random() * 101));
                        damage = defaults.player.Attack + defaults.player.CurrentWeapon.WeaponDamage;
                        if (rnd < defaults.player.CurrentWeapon.WeaponCrit) {
                            damage = damage * 1.30;
                        }

                        if (damage > defaults.currentRoom.HumanEnemies[j].Defence) {
                            damage -= defaults.currentRoom.HumanEnemies[j].Defence;
                            defaults.currentRoom.HumanEnemies[j].Health -= damage;

                            //give the player XP for the damage inflicted
                            $.post('/Map/AddPlayerXP', { xp: damage });
                            defaults.player.XP += damage;
                            if (defaults.currentRoom.HumanEnemies[j].Health <= 0) {
                                defaults.currentRoom.HumanEnemies.splice(j, 1);
                                j--;
                                render();
                            }
                        }
                    }
                }
                for (var k = 0; k < defaults.currentRoom.BossEnemies.length; k++) {
                    if ((defaults.currentRoom.BossEnemies[k].X === enemyX) &&
                        (defaults.currentRoom.BossEnemies[k].Y === enemyY)) {
                        rnd = Math.floor((Math.random() * 101));
                        damage = defaults.player.Attack + defaults.player.CurrentWeapon.WeaponDamage;
                        if (rnd < defaults.player.CurrentWeapon.WeaponCrit) {
                            damage = damage * 1.30;
                        }

                        if (damage > defaults.currentRoom.BossEnemies[k].Defence) {
                            damage -= defaults.currentRoom.BossEnemies[k].Defence;
                            defaults.currentRoom.BossEnemies[k].Health -= damage;

                            //give the player XP for the damage inflicted
                            $.post('/Map/AddPlayerXP', { xp: damage });
                            defaults.player.XP += damage;
                            if (defaults.currentRoom.BossEnemies[k].Health <= 0) {
                                defaults.currentRoom.BossEnemies.splice(k, 1);
                                k--;
                                render();
                            }
                        }
                    }
                }
            }
        }

        //check what is on a given position {enemy, weapon, trap, empty}
        function getOccupation(x, y) {
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

            for (var l = 0; l < defaults.currentRoom.WeaponOnGrounds.length; l++) {
                if ((defaults.currentRoom.WeaponOnGrounds[l].X === x) &&
                    (defaults.currentRoom.WeaponOnGrounds[l].Y === y)) {
                    return "weapon";
                }
            }
            for (var m = 0; m < defaults.currentRoom.Traps.length; m++) {
                if ((defaults.currentRoom.Traps[m].X === x) &&
                    (defaults.currentRoom.Traps[m].Y === y)) {
                    return "trap";
                }
            }

            return "empty";
        }

        function pickupWeapon(weaponID) {
            for (var i = 0; i < defaults.currentRoom.WeaponOnGrounds.length; i++) {
                if (defaults.currentRoom.WeaponOnGrounds[i].WeaponID === weaponID) {
                    defaults.currentRoom.WeaponOnGrounds[i].WeaponID = defaults.player.CurrentWeapon.ID; //change the weapon on the ground to be the current weapon of the player

                    $.post('/Map/NewWeapon', { weaponID: weaponID }, function(data) {
                        $.extend(true, defaults.player.CurrentWeapon, data); //update the player's weapon
                    });
                }
            }
        }

        //Speler controls
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
                                if ((occ === "trap")) {
                                    for (var i = 0; i < defaults.currentRoom.Traps.length; i++) {
                                        if ((defaults.currentRoom.Traps[i].X === defaults.player.X) &&
                                            (defaults.currentRoom.Traps[i].Y === defaults.player.Y)) {
                                            defaults.player.Health -= defaults.currentRoom.Traps[i].Damage;
                                            alert("ITS A TRAP! (-" + defaults.currentRoom.Traps[i].Damage + " hp)");
                                            defaults.currentRoom.Traps[i].Damage = 0;
                                            $.post('/Map/UpdatePlayerHealth', { hp: defaults.player.Health });
                                        }
                                    }
                                }
                            } else if (occ === "enemy") {
                                attackEnemy(defaults.player.X, pos);
                            } else if (occ === "weapon") {
                                for (var m = 0; m < defaults.currentRoom.WeaponOnGrounds.length; m++) {
                                    if ((defaults.currentRoom.WeaponOnGrounds[m].X === defaults.player.X) &&
                                        (defaults.currentRoom.WeaponOnGrounds[m].Y === pos)) {
                                        pickupWeapon(defaults.currentRoom.WeaponOnGrounds[m].WeaponID);
                                    }
                                }
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
                                if ((occ === "trap")) {
                                    for (var j = 0; j < defaults.currentRoom.Traps.length; j++) {
                                        if ((defaults.currentRoom.Traps[j].X === defaults.player.X) &&
                                            (defaults.currentRoom.Traps[j].Y === defaults.player.Y)) {
                                            defaults.player.Health -= defaults.currentRoom.Traps[j].Damage;
                                            alert("ITS A TRAP! (-" + defaults.currentRoom.Traps[j].Damage + " hp)");
                                            defaults.currentRoom.Traps[j].Damage = 0;
                                            $.post('/Map/UpdatePlayerHealth', { hp: defaults.player.Health });
                                        }
                                    }
                                }
                            } else if (occ === "enemy") {
                                attackEnemy(pos, defaults.player.Y);
                            } else if (occ === "weapon") {
                                for (var n = 0; n < defaults.currentRoom.WeaponOnGrounds.length; n++) {
                                    if ((defaults.currentRoom.WeaponOnGrounds[n].X === pos) &&
                                        (defaults.currentRoom.WeaponOnGrounds[n].Y === defaults.player.Y)) {
                                        pickupWeapon(defaults.currentRoom.WeaponOnGrounds[n].WeaponID);
                                    }
                                }
                            }
                        } else if (defaults.currentRoom.Clear && defaults.currentRoom.RoomID !== defaults.currentRoom.NextRoomID) {
                            $.post('/Map/ChangedRoom', { roomID: defaults.currentRoom.NextRoomID });
                            location.reload(true);
                        }
                        break;
                    case 37: //Left
                        pos = defaults.player.X - defaults.tileSize;
                        occ = getOccupation(pos, defaults.player.Y);
                        if (pos > 0) {
                            if ((occ === "trap") || (occ === "empty")) {
                                defaults.player.X -= defaults.tileSize;
                                defaults.player.MovePoints = 0;
                                if ((occ === "trap")) {
                                    for (var k = 0; k < defaults.currentRoom.Traps.length; k++) {
                                        if ((defaults.currentRoom.Traps[k].X === defaults.player.X) &&
                                            (defaults.currentRoom.Traps[k].Y === defaults.player.Y)) {
                                            defaults.player.Health -= defaults.currentRoom.Traps[k].Damage;
                                            alert("ITS A TRAP! (-" + defaults.currentRoom.Traps[k].Damage + " hp)");
                                            defaults.currentRoom.Traps[k].Damage = 0;
                                            $.post('/Map/UpdatePlayerHealth', { hp: defaults.player.Health });
                                        }
                                    }
                                }
                            } else if (occ === "enemy") {
                                attackEnemy(pos, defaults.player.Y);
                            } else if (occ === "weapon") {
                                for (var o = 0; o < defaults.currentRoom.WeaponOnGrounds.length; o++) {
                                    if ((defaults.currentRoom.WeaponOnGrounds[o].X === pos) &&
                                        (defaults.currentRoom.WeaponOnGrounds[o].Y === defaults.player.Y)) {
                                        pickupWeapon(defaults.currentRoom.WeaponOnGrounds[o].WeaponID);
                                    }
                                }
                            }
                        } else if (defaults.currentRoom.Clear && defaults.currentRoom.RoomID !== defaults.currentRoom.PreviousRoomID) {
                            $.post('/Map/ChangedRoom', { roomID: defaults.currentRoom.PreviousRoomID });
                            location.reload(true);
                        }
                        break;
                    case 40: //Down
                        pos = defaults.player.Y + defaults.tileSize;
                        occ = getOccupation(defaults.player.X, pos);
                        if (pos <= defaults.roomHeight) {
                            if ((occ === "trap") || (occ === "empty")) {
                                defaults.player.Y += defaults.tileSize;
                                defaults.player.MovePoints = 0;
                                if ((occ === "trap")) {
                                    for (var l = 0; l < defaults.currentRoom.Traps.length; l++) {
                                        if ((defaults.currentRoom.Traps[l].X === defaults.player.X) &&
                                            (defaults.currentRoom.Traps[l].Y === defaults.player.Y)) {
                                            defaults.player.Health -= defaults.currentRoom.Traps[l].Damage;
                                            alert("ITS A TRAP! (-" + defaults.currentRoom.Traps[l].Damage + " hp)");
                                            defaults.currentRoom.Traps[l].Damage = 0;
                                            $.post('/Map/UpdatePlayerHealth', { hp: defaults.player.Health });
                                        }
                                    }
                                }
                            } else if (occ === "enemy") {
                                attackEnemy(defaults.player.X, pos);
                            } else if (occ === "weapon") {
                                for (var p = 0; p < defaults.currentRoom.WeaponOnGrounds.length; p++) {
                                    if ((defaults.currentRoom.WeaponOnGrounds[p].X === defaults.player.X) &&
                                        (defaults.currentRoom.WeaponOnGrounds[p].Y === pos)) {
                                        pickupWeapon(defaults.currentRoom.WeaponOnGrounds[p].WeaponID);
                                    }
                                }
                            }
                        }
                        break;
                }
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
        game.playerRegen();
        game.updateInfo();
    }

    var sound = document.getElementById("background_music");
    sound.loop = true;
    sound.play();
});