import React, { useState } from 'react';
import ListGroup from 'react-bootstrap/ListGroup';
import Button from 'react-bootstrap/Button';
import CreateProjectModal from '../CreateProjectModal';
import * as css from "./StylesProject.css";
import { BootstrapTable, TableHeaderColumn } from 'react-bootstrap-table';
import projectsActions from '../../store/Actions'
// with es6
import '../../../node_modules/react-bootstrap-table/dist/react-bootstrap-table-all.min.css';


const proj = {
    id: 1,
    title: "Yee",
    description: "!!!!",
    dateCreate: "12.01.2019",
    countUsers: 10,
    creator: "Барабашка",
    users: [
        {
            id: 1,
            firstName: "Inna",
            lastName: "Shutova",
            countActiveTasks: 19,
            email: "i.schutova@nordclan.com"
        },
        {
            id: 2,
            firstName: "Natalia",
            lastName: "Myasnikova",
            countActiveTasks: 19,
            email: "i.schutova@nordclan.com"
        }
    ]
}
    ;

const Project = () => {
    const [show, setShow] = useState(false);


    return <div className="main_fon">
        <div className="inline_block">
            <div className="title_block">
                <div className="text_title"> {proj.title} </div>
                <br />
                <div className="text_author">
                    Автор: {proj.creator}
                </div>
            </div>

            <Button className="button_padding" variant="outline-primary">Выгрузить статистику</Button>
        </div>
        <div className="text_description">
            {proj.description}
        </div>

        {
            proj.users.lenth !== 0 && <div className="users_list">
                <div className="inline_block_users_title">Список пользователей</div>
                {
                    proj.users.map(user => {
                        return <ListGroup.Item className="inline_block_elements">
                            <div className="inline_block_users">{user.firstName}</div>
                            <div className="inline_block_users">{user.lastName}</div>
                            <div className="inline_block_users">{user.email}</div>
                        </ListGroup.Item>;
                    })
                }
            </div>
        }

        <div className="inline_block_users_title">Задачи по проекту</div>
        <div className="text_description">
            <BootstrapTable data={[]} striped hover>
                <TableHeaderColumn isKey dataField='taskId'>Id задачи</TableHeaderColumn>
                <TableHeaderColumn dataField='name'>Название задачи</TableHeaderColumn>
                <TableHeaderColumn dataField='taskDate'>Дата постановки</TableHeaderColumn>
                <TableHeaderColumn dataField='id'>Id исполнителя</TableHeaderColumn>
                <TableHeaderColumn dataField='time'>Затраченное время</TableHeaderColumn>
                <TableHeaderColumn dataField='status'>Статус</TableHeaderColumn>
            </BootstrapTable>
        </div>



        <CreateProjectModal show={show} onHide={() => setShow(false)} />
    </div>;
}

export default Project;