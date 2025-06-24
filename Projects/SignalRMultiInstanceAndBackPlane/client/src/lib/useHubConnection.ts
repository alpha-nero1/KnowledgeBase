import * as signalR from '@microsoft/signalr';
import { useEffect, useState } from 'react';

const hubUrl = 'http://localhost:5000/chathub';

const receiveMessageKey = "ReceiveMessage";
const sendMessageKey = "SendMessage";
type Message = { user: string, message: string };

export const useHubConnection = (username: string, isLoggedIn: boolean) => {
    const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
    const [messages, setMessages] = useState<Message[]>([]);
    const [connectionSevered, setConnectionSevered] = useState(false);
    const [isLoading, setIsLoading] = useState(false);

    // THIS is how you send an access token to the API server.
    const hubOptions = {
        accessTokenFactory: () => localStorage.getItem("access_token") ?? ''
    }

    const connect = () => {
        if (!isLoggedIn) return;
        const _connection = new signalR.HubConnectionBuilder()
            .withUrl(`${hubUrl}?user=${username}`, hubOptions)
            // SignalR handles the reconnection logic here.
            .withStatefulReconnect()
            .build();

        // Add our SignalR lifecycle hooks here!
        // on reconnecting.
        _connection.onreconnecting(err => {
            console.warn("Lost SignalR connection, attempting to reconnect: ", err);
            setConnectionSevered(true);
        });

        // on reconnected!
        _connection.onreconnected(connectionId  => {
            console.log("Reconnected! New connectionId: ", connectionId);
            setConnectionSevered(false);
        });

        // Connection completely closed.
        _connection.onclose(err => {
            console.error("SignalR connection closed for good:", err);
            // retries exhausted or stop was called
            setConnectionSevered(true);
        });

        // Regular handlers after lifecylce handler setup.
        _connection.on(receiveMessageKey, (user, message) => {
            setMessages(prev => [...prev, { user, message }]);
        });

        setIsLoading(true);
        _connection
            .start()
            .then(() => {
                console.log('Connected to SignalR hub');
                // The magica!
                setConnectionSevered(false);
                setIsLoading(false);
            })
            .catch(err => {
                console.error('Connection to SignalR hub severed', err);
                setConnectionSevered(true);
                setIsLoading(false);
            });
        
        setConnection(_connection);
    }

    useEffect(() => {
        connect();

        return () => {
            setMessages([]);
        }
    }, [username, isLoggedIn]);
    
    const sendMessage = async (recipient: string, message: string) => {
        if (!connection || !username) return;
        await connection.invoke(sendMessageKey, recipient, message);
        setMessages(prev => [...prev, { user: username, message }]);
    }

    return {
        connection,
        messages,
        sendMessage,
        connectionSevered,
        isLoading,
        connect
    };
}