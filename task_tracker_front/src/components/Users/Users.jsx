import React, {useState} from 'react';
import ListGroup from 'react-bootstrap/ListGroup';
import Button from 'react-bootstrap/Button'
import * as css from "./Styles.css";
import CreateUserModal from '../CreateUserModal/CreateUserModal';


const users = [
    {
        firstName:"Inna",
        lastName:"Shutova",
        dateRegistration:"12.12.12",
        countActiveTasks:19,
        email: "i.schutova@nordclan.com"
    },
    {
        firstName:"Natalia",
        lastName:"Myasnikova",
        dateRegistration:"12.12.12",
        countActiveTasks:19,
        email: "natali@nordclan.com"
    }
];

const Users = () => {
    const [show, setShow] = useState(false);


    return <div className="main">
        <div className="inline_block">
            <div className="text_title">Пользователи: </div>
            <Button className="button_padding" variant="outline-primary">Выгрузить статистику</Button>
            <Button className="button_padding" variant="outline-primary" onClick={()=>setShow(true)}>Добавить пользователя</Button>
        </div>

        <ListGroup>
            <ListGroup.Item className="inline_block">
                <div className="inline_element" href="/proj">Имя</div>
                <div className="inline_element">Фамилия</div>
                <div className="inline_element">Дата регистрации</div>
                <div className="inline_element">email</div>
                <div className="inline_element">Количество незакрытых задач</div>
            </ListGroup.Item>
            {
                users.map(usr => (
                    <ListGroup.Item className="inline_block">
                        <a className="inline_element" href="/proj">{usr.firstName}</a>
                        <div className="inline_element">{usr.lastName}</div>
                        <div className="inline_element">{usr.dateRegistration}</div>
                        <div className="inline_element">{usr.email}</div>
                        <div className="inline_element">{usr.countActiveTasks}</div>
                    </ListGroup.Item>
                ))
            }
        </ListGroup>
        <CreateUserModal show={show} onHide={() => setShow(false)} />
    </div>;
}

export default Users;