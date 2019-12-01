import React from 'react';
import ListGroup from 'react-bootstrap/ListGroup';
import Button from 'react-bootstrap/Button';
import * as css from "./Styles.css";

const proj = [
    {
        title: "Yee",
        description: "!!!!",
        dateCreate: "12.01.2019",
        countUsers: 10,
        creator: "Барабашка"
    }, {
        title: "Ye1e",
        description: "!!!!",
        dateCreate: "12.01.2019",
        countUsers: 5,
        creator: "Джокер"
    }
];

const Projects = () => {
    return <div className="main">
        <div className="inline_block">
            <div className="text_title">Ваши проекты: </div>
            <Button className="button_padding" variant="outline-primary">Выгрузить статистику</Button>
            <Button className="button_padding" variant="outline-primary">Создать проект</Button>
        </div>

        <ListGroup>
            <ListGroup.Item className="inline_block">
                <div className="inline_element" href="/proj">Название проекта</div>
                <div className="inline_element">Дата создания</div>
                <div className="inline_element">Количество пользователей</div>
                <div className="inline_element">Создатель</div>
            </ListGroup.Item>
            {
                proj.map(pr => (
                    <ListGroup.Item className="inline_block">
                        <a className="inline_element" href="/proj">{pr.title}</a>
                        <div className="inline_element">{pr.dateCreate}</div>
                        <div className="inline_element">{pr.countUsers}</div>
                        <div className="inline_element">{pr.creator}</div>
                    </ListGroup.Item>
                ))
            }
        </ListGroup>
    </div>;
}

export default Projects;