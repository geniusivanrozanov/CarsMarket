import { Container } from "react-bootstrap"
import { useParams } from "react-router-dom"

const Chat = () => {
    const {chatId} = useParams()

    return (
        <Container>
            <h1>Chat {chatId}</h1>
        </Container>
    )
}

export default Chat