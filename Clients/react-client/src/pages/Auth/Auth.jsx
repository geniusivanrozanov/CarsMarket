import { useState } from "react";
import { Button, Container, Form } from "react-bootstrap";
import { IDENTITY_URL } from "../../constants/urls";

const Auth = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const url = IDENTITY_URL + "/api/users/login"

    const handleSubmit = async (e) => {
        e.preventDefault()

        let res = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                email,
                password
            }),
        });

        const data = await res.json();

        localStorage.setItem("access_token", data.accessToken)
        localStorage.setItem("refresh_token", data.refreshToken)
    }

    return (
        <>
            <Container>
                <h1>Login</h1>
                <Form>
                    <Form.Group className="mb-3" controlId="formBasicEmail">
                        <Form.Label>Email address</Form.Label>
                        <Form.Control
                            type="email"
                            placeholder="Enter email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                        />
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="formBasicPassword">
                        <Form.Label>Password</Form.Label>
                        <Form.Control
                            type="password"
                            placeholder="Password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                    </Form.Group>
                    <Button variant="primary" type="submit" onClick={handleSubmit}>
                        Submit
                    </Button>
                </Form>
            </Container>
        </>
    );
};

export default Auth;
