import React, { useState } from 'react';
import ListGroup from 'react-bootstrap/ListGroup';
import Button from 'react-bootstrap/Button';
import CreateProjectModal from '../CreateProjectModal';
import * as css from "./StylesProjects.css";

const proj = [
    {
        id: 1,
        title: "Yee",
        description: "!!!!",
        dateCreate: "12.01.2019",
        countUsers: 10,
        creator: "Барабашка"
    }, {
        id: 2,
        title: "Ye1e",
        description: "!!!!",
        dateCreate: "12.01.2019",
        countUsers: 5,
        creator: "Джокер"
    }
];

const Projects = () => {
    const [show, setShow] = useState(false);


    return <div className="main_fon">
        <div className="inline_block">
            <div className="text_title_ag">Ваши проекты: </div>
            <Button className="button_padding" variant="outline-primary">Выгрузить статистику</Button>
            <Button className="button_padding" variant="outline-primary" onClick={() => setShow(true)}>Создать проект</Button>
        </div>

        <ListGroup>
            <ListGroup.Item className="inline_block">
                <div className="inline_element">Название проекта</div>
                <div className="inline_element">Дата создания</div>
                <div className="inline_element">Количество пользователей</div>
                <div className="inline_element">Создатель</div>
            </ListGroup.Item>
            {
                proj.map(pr => (
                    <ListGroup.Item className="inline_block">
                        <a className="inline_element" href={"/project/" + pr.id}>{pr.title}</a>
                        <div className="inline_element">{pr.dateCreate}</div>
                        <div className="inline_element">{pr.countUsers}</div>
                        <div className="inline_element">{pr.creator}</div>
                    </ListGroup.Item>
                ))
            }
        </ListGroup>
        <CreateProjectModal show={show} onHide={() => setShow(false)} />
    </div>;
}

export default Projects;