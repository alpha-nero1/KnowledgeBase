import * as signalR from '@microsoft/signalr';
import { useEffect, useState } from 'react';

const hubUrl = 'http://localhost:5000/chatHub';

const receiveMessageKey = "ReceiveMessage";
const sendMessageKey = "SendMessage";
type Message = { user: string, message: string };

export const useHubConnection = (userId: string) => {
    const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
    const [messages, setMessages] = useState<Message[]>([]);

    useEffect(() => {
        const _connection = new signalR.HubConnectionBuilder()
            .withUrl(hubUrl)
            .withStatefulReconnect()
            .build();
        
        _connection
            .start()
            .then(() => {
                console.log('Connected to SignalR hub');
                // The magica!
                _connection.on(receiveMessageKey, (user, message) => {
                    setMessages(prev => [...prev, { user, message }]);
                });
            })
            .catch(err => {
                console.error('Connection to SignalR hub severed', err);
            })
        
        setConnection(_connection);

        return () => {
            setMessages([]);
        }
    }, [userId]);
    
    const sendMessage = async (message: string) => {
        if (!connection || !userId) return;
        await connection.invoke(sendMessageKey, userId, message);
    }

    return {
        connection,
        messages,
        sendMessage
    };
}