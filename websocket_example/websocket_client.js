class websocket_client {
    constructor() {
        this.socket = null;
        this.isConnected = false;
        this.eventHandlers = {};
    }

    connect(wsUrl) {
        return new Promise((resolve, reject) => {
            this.socket = new WebSocket(wsUrl);

            this.socket.addEventListener('open', (event) => {
                console.log('Connected to WebSocket server');
                this.isConnected = true;
                resolve();
            });

            this.socket.addEventListener('message', (event) => {
                this.recieve_message(event.data);
            });

            this.socket.addEventListener('error', (event) => {
                console.error('WebSocket error:', event);
                reject(event);
            });

            this.socket.addEventListener('close', (event) => {
                console.log('WebSocket connection closed:', event);
                this.isConnected = false;
                if (this.eventHandlers['close']) {
                    this.eventHandlers['close'](event);
                }
            });
        });
    }

    disconnect() {
        if (this.socket) {
            this.socket.close();
        }
        this.isConnected = false;
    }

    send_message(message) {
        if (this.isConnected && this.socket.readyState === WebSocket.OPEN) {
            this.socket.send(JSON.stringify(message));
        } else {
            console.warn('WebSocket is not connected.');
        }
    }

    recieve_message(data) {
        const responseWrapper = JSON.parse(data);

        // Extract the response data
        const messageType = responseWrapper.message_type;
        const responseType = responseWrapper.response_type;
        const response = responseWrapper.response;

        if (responseType === 'error') {
            console.error('Error:', response);
            if (this.eventHandlers['error']) {
                this.eventHandlers['error'](response);
            }
            return;
        }

        // Use switch case to handle different message types
        switch (messageType) {
            case 'get_life_cycle':
                if (this.eventHandlers['life_cycle']) {
                    this.eventHandlers['life_cycle'](response);
                }
                break;
            case 'get_players':
                if (this.eventHandlers['get_players']) {
                    this.eventHandlers['get_players'](response);
                }
                break;
            case 'get_player':
                if (this.eventHandlers['get_player']) {
                    this.eventHandlers['get_player'](response);
                }
                break;
            default:
                console.warn('Unhandled message type:', messageType);
                if (this.eventHandlers['unhandled']) {
                    this.eventHandlers['unhandled'](messageType, responseType, response);
                }
                break;
        }
    }

    add_message_recieved_callback(eventType, handler) {
        this.eventHandlers[eventType] = handler;
    }

    request_life_cycle()
    {
        const message = {
            message_type: 'get_life_cycle',
            arguments: {}
        };
        this.send_message(message);
    }

    request_players(type)
    {
        const message = {
            message_type: 'get_players',
            arguments: {
                type: type
            }
        };

        this.send_message(message);
    }

    request_player(type, name)
    {
        const message = {
            message_type: 'get_player',
            arguments: {
                type: type,
                player: name
            }
        };

        this.send_message(message);
    }
}