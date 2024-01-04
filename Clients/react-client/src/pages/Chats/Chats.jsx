import { useContext, useEffect } from "react";
import { CHAT_URL } from "./../../constants/urls";
import ChatContext from "../../contexts/ChatContext";
import { Col, Container, Row } from "react-bootstrap";
import ChatMin from "../../components/ChatMin/ChatMin";

const Chats = () => {
    const { chats, setChats } = useContext(ChatContext);
    const url = CHAT_URL + "/api/chats";

    useEffect(() => {
        const loadChats = async () => {
            let res = await fetch(url, {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${localStorage.getItem(
                        "access_token"
                    )}`,
                },
            });

            const data = await res.json();

            setChats(data);
        };

        loadChats();
    }, []);

    return (
        <>
            <h1>Hello</h1>
            <Container fluid>
                {chats.map((chat) => (
                    <Row key={chat.id}>
                        <Col lg>
                            <ChatMin chat={chat} />
                        </Col>
                    </Row>
                ))}
            </Container>
        </>
    );
};

export default Chats;
