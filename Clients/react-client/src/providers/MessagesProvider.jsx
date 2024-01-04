import { useState } from "react";
import { HubConnectionBuilder } from "@microsoft/signalr";
import MessagesContext from "../contexts/MessagesContext";

const MessagesProvider = ({ children }) => {
    const [messages, setMessages] = useState([])

    const hubConnection = new HubConnectionBuilder()
            .withUrl("ws://localhost:8003/messages")
            .build();

    return (
        <MessagesContext.Provider value={{messages, setMessages}}>
            {children}
        </MessagesContext.Provider>
    )
}

export default MessagesProvider;