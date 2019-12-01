
import 'bootstrap/dist/css/bootstrap.min.css';
import React from 'react';
import { Navbar, NavItem, Nav, Grid, Row, Col } from "react-bootstrap";


const Header = () => {
    return <div>
        <Navbar collapseOnSelect expand="lg" bg="light" variant="light">
            <Navbar.Brand href="#home">TaskTracker</Navbar.Brand>
            <Navbar.Toggle aria-controls="responsive-navbar-nav" />
            <Navbar.Collapse id="responsive-navbar-nav">
                <Nav className="mr-auto">
                    <Nav.Link href="/projects">Проекты</Nav.Link>
                    <Nav.Link href="/users">Пользователи</Nav.Link>
                    <Nav.Link href="#pricing">Отчеты</Nav.Link>

                </Nav>
                <Nav>
                    <Nav.Link href="/userProfile">Личный кабинет</Nav.Link>
                    <Nav.Link eventKey={2} href="/login">
                        Выйти
      </Nav.Link>
                </Nav>
            </Navbar.Collapse>
        </Navbar>

    </div>;
}

export default Header;