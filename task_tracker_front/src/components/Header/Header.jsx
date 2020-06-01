
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { Component } from 'react';
import { Navbar, NavItem, Nav, Grid, Row, Col, Button } from "react-bootstrap";
import { connect } from 'react-redux';
import storage from 'redux-persist/lib/storage';


class Header extends Component {

    handlerExit = () => {
        storage.removeItem('persist:userRoot');
        storage.removeItem('persist:root');
        this.props.history.push(`/`);
    }

    render() {
        return <div>
            <Navbar collapseOnSelect expand="lg" bg="light" variant="light">
                <Navbar.Brand href="#home">TaskTracker</Navbar.Brand>
                <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                <Navbar.Collapse id="responsive-navbar-nav">
                    <Nav className="mr-auto">
                        <Nav.Link href="/projects">Проекты</Nav.Link>
                        {
                            this.props.userReduser.is_admin &&
                                <Nav.Link href="/users">Пользователи</Nav.Link>
                        }
                       
                        <Nav.Link href="/tasks">Задачи</Nav.Link>
                        <Nav.Link href="/timesheets">Отчеты по времени</Nav.Link>
                    </Nav>
                    <Nav>
                        <Nav.Link href="/userProfile">Личный кабинет</Nav.Link>
                        <Button  eventKey={2} href="/login" onClick={this.handlerExit}>
                            Выйти
      </Button>
                    </Nav>
                </Navbar.Collapse>
            </Navbar>

        </div>;
    }
}
const mapStateToProps = (state) => ({
    userReduser: state.UserReduser
});

export default connect(
    mapStateToProps,
    null) (Header);