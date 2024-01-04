import React from "react";
import ReactDOM from "react-dom/client";
import Chats from "./pages/Chats/Chats.jsx";
import Chat from "./pages/Chat/Chat.jsx";
import ChatProvider from "./providers/ChatProvider.jsx";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import "./index.css";
import "bootstrap/dist/css/bootstrap.min.css";
import Root from "./pages/Root/Root.jsx";
import Auth from "./pages/Auth/Auth.jsx";
import MessagesProvider from "./providers/MessagesProvider.jsx";

const router = createBrowserRouter([
    {
        path: "/",
        element: <Root />,
    },
    {
        path: "/auth",
        element: <Auth />,
    },
    {
        path: "/chats",
        element: <Chats />,
    },
    {
      path: "/chats/:chatId",
      element: <Chat />,
  },
]);

ReactDOM.createRoot(document.getElementById("root")).render(
    <React.StrictMode>
        <ChatProvider>
            <MessagesProvider>
                <RouterProvider router={router} />
            </MessagesProvider>
        </ChatProvider>
    </React.StrictMode>
);
