import { useState } from "react"
import ChatContext from "../contexts/ChatContext"

const ChatProvider = ({ children }) => {
    const [chats, setChats] = useState([])

    return (
        <ChatContext.Provider value={{chats, setChats}}>
            {children}
        </ChatContext.Provider>
    )
}

export default ChatProvider;