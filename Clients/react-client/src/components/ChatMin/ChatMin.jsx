import { Card } from "react-bootstrap";

const ChatMin = ( {chat} ) => {
    return (
        <Card>
            <a href={"/chats/" + chat.id }>
                <Card.Title>{chat.id}</Card.Title>
                <Card.Body className="d-flex justify-content-between">
                    <span>{chat.lastMessage?.text ?? "Empty dialog"}</span>
                    <span>{chat.lastMessage?.createdAt ?? "No time"}</span>
                </Card.Body>
            </a>
        </Card>
    );
};

export default ChatMin;
