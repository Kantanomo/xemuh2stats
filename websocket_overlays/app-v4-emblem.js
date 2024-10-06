let client = new websocket_client();
let update_handle = null;
let current_life_cycle = "none";

// Store the previous respawn state for each player
let previous_respawn_timer = {};

function players_sort_team(a, b) {
    if (a.player.team_index < b.player.team_index) {
        return -1;
    }
    if (a.player.team_index > b.player.team_index) {
        return 1;
    }
    return 0;
}

client.add_message_recieved_callback('life_cycle', (life_cycle_state) => {
    current_life_cycle = life_cycle_state.life_cycle;
});

client.add_message_recieved_callback('get_players', (players_data) => {
    let player_names = Object.keys(players_data);

    let red_players = {};
    let blue_players = {};

    player_names.forEach(player_name => {
        const player = players_data[player_name];
        
        if (player.team.includes('red')) {
            red_players[player_name] = player; // Correctly assign to red_players
        }
        
        if (player.team.includes('blue')) {
            blue_players[player_name] = player; // Correctly assign to blue_players
        }
    });

    let red_table_rows = document.querySelectorAll('#red-team-table tbody tr');
    let blue_table_rows = document.querySelectorAll("#blue-team-table tbody tr");

    for (let i = 0; i < 4; i++) {
        if (Object.keys(red_players).length > i) {
            let red_row = red_table_rows[i];
            let red_cells = red_row.querySelectorAll('td');
            let player_name = Object.keys(red_players)[i];
            let player = red_players[player_name];

            // Handle respawn_timer changes
            handleRespawnTimer(red_row, red_cells, player, player_name, '#A10000', '#800000');

            // Add emblem to column 0
            red_cells[0].innerHTML = getEmblemCellContent(player); 

            // Update player info in the next columns, including weapon image in player_name cell
            red_cells[1].innerHTML = getPlayerWithWeaponContent(player_name, player.current_weapon);
            red_cells[2].textContent = player.kills;
            red_cells[3].textContent = player.assists;
            red_cells[4].textContent = player.deaths;
        } else {
            let red_cells = red_table_rows[i].querySelectorAll('td');
            red_cells.forEach(cell => cell.textContent = "");
        }

        if (Object.keys(blue_players).length > i) {
            let blue_row = blue_table_rows[i];
            let blue_cells = blue_row.querySelectorAll('td');
            let player_name = Object.keys(blue_players)[i];
            let player = blue_players[player_name];

            // Handle respawn_timer changes
            handleRespawnTimer(blue_row, blue_cells, player, player_name, '#2A4C91', '#1A3A75');

            // Add emblem to column 3
            blue_cells[3].innerHTML = getEmblemCellContent(player); 

            // Update player info in the next columns, including weapon image in player_name cell
            blue_cells[4].innerHTML = getPlayerWithWeaponContent(player_name, player.current_weapon);
            blue_cells[0].textContent = player.kills;
            blue_cells[1].textContent = player.assists;
            blue_cells[2].textContent = player.deaths;
        } else {
            let blue_cells = blue_table_rows[i].querySelectorAll('td');
            blue_cells.forEach(cell => cell.textContent = "");
        }
    }
});

function handleRespawnTimer(row, cells, player, player_name, original_background, respawn_background) {
    // Check the respawn timer value
    if (parseInt(player.respawn_timer) !== 0) {
        // If the respawn timer is greater than 0, change the row background and text color
        row.style.backgroundColor = respawn_background; // Darker color when in respawn
        cells.forEach(cell => cell.style.color = '#BBBBBB'); // Darker text shade
    } else if (previous_respawn_timer[player_name] !== undefined && previous_respawn_timer[player_name] !== 0) {
        // When the respawn timer goes back to 0, revert the row background and text color
        row.style.backgroundColor = original_background; // Revert background color
        cells.forEach(cell => cell.style.color = 'white'); // Revert text color
    }

    // Update the previous respawn timer value for the player
    previous_respawn_timer[player_name] = parseInt(player.respawn_timer);
}

function update() {
    if (current_life_cycle == 'in_game') {
        client.request_players("scoreboard");
    }
    client.request_life_cycle();
}

function start() {
    update_handle = setInterval(update, 500);
}

function stop() {
    if (update_handle) {
        clearInterval(update_handle);
        update_handle = null;
    }
}

document.addEventListener("DOMContentLoaded", function () {
    client.connect(`ws://${window.config.host}:${window.config.port}`).then(() => {
        start();
    }).catch((error) => {
        alert("WebSocket Connection Error");
        console.error("Connection Error:", error);
    });
});

// Function to generate HTML content for the emblem image
function getEmblemCellContent(player) {
    // Construct the emblem URL using player emblem values
    const emblem_url = `https://www.halo2pc.com/test-pages/CartoStat/Emblem/emblem.php?P=${player.primary_color}&S=${player.secondary_color}&EP=${player.tertiary_color}&ES=${player.quaternary_color}&EF=${player.emblem_foreground}&EB=${player.emblem_background}&ET=0`;

    // Return emblem image
    return `<img src="${emblem_url}" style="max-height: 44px;" alt="Emblem" />`;
}

// Function to generate HTML content for player name with weapon image
function getPlayerWithWeaponContent(player_name, weapon) {
    // Construct the weapon image URL
    const weapon_image_file = `Weapons/${weapon.replace(/\s+/g, '')}.png`;

    // Return HTML content with player name and weapon image
    return `
        <div style="display: flex; justify-content: space-between; align-items: center;">
            <span style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">${player_name}</span>
            <img src="${weapon_image_file}" style="max-height: 20px; margin-left: 5px;" alt="Weapon" />
        </div>
    `;
}
