import React, { Component } from 'react';
import ListGroup from 'react-bootstrap/ListGroup';
import Button from 'react-bootstrap/Button';
import CreateProjectModal from '../CreateProjectModal';
import { connect } from 'react-redux';
import ActionTypes from '../../store/Actions/index.js';
import axios from 'axios';

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


class Projects extends Component {
    constructor(props) {
        super(props);
        // Не делайте этого!
        this.state = {
            projects: [],
            isShow: false
        };
    }

    componentDidMount = () => {
        axios.get('http://localhost:57392/project?apikey=qwerty').then(response => {
            console.log(response);
            this.props.projectsSaveData(response.data);
        });
    };


    setShow = (value) => {
        this.setState({ isShow: value });
    }


    render() {

        this.props.projectsRequest();
        console.log(this.props);
        console.log(this.state);
        return <div className="main_fon">
            <div className="inline_block">
                <div className="text_title_ag">Ваши проекты: </div>
                <Button className="button_padding" variant="outline-primary">Выгрузить статистику</Button>
                <Button className="button_padding" variant="outline-primary" onClick={() => this.setShow(true)}>Создать проект</Button>
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
            <CreateProjectModal show={this.state.isShow} onHide={() => this.setShow(false)} />
        </div>;
    }

}

const mapStateToProps = (state) => ({
    projects: state.Projects,
    isShow: false
});

const mapDispatchToProps = (dispatch) => {
    return {
        projectsRequest: () => {
            dispatch(ActionTypes.ActionTypes.projectsActions.projectsActions.projectsRequest());
        },
        projectsSaveData: (action) => {
            dispatch(ActionTypes.ActionTypes.projectsActions.projectsActions.projectsSaveData(action));
        }
    }
};


export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Projects);