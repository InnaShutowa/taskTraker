import React, { Component } from 'react';
import ListGroup from 'react-bootstrap/ListGroup';
import Button from 'react-bootstrap/Button';
import CreateProjectModal from '../CreateProjectModal';
import { connect } from 'react-redux';
import ActionTypes from '../../store/Actions/index.js';
import axios from 'axios';
import * as css from "./TimesheetsStyles.css";
import Moment from 'react-moment';


class Timesheets extends Component {
    constructor(props) {
        super(props);
        this.state = {
            timesheets: [],
            isShow: false,
            isRequested: false
        };
    }

    componentDidMount = () => {
        this.getTimesheetsHandler( 1);
    };

    getTimesheetsHandler = ( userId) => {
        axios.get('http://localhost:57392/timesheet?apikey='+this.props.user.apikey+'&user_id=1')
            .then(response => {
                if (response.data.error) {
                    alert(response.data.error);
                } else {
                    this.setState({ timesheets: response.data.data, isRequested: true });
                }
            });
    }

    deleteTimesheetHandler = (timesheetId) => {
        axios.post('http://localhost:57392/timesheetJobs',
            {
                apikey: this.props.user.apikey,
                timesheet_id: timesheetId

            }).then(response => {
                if (response.data.error) {
                    alert(response.data.error);
                } else {
                    this.setShow(false);
                    this.getTimesheetsHandler(1);
                }
            });
    }

    setShow = (value) => {
        this.setState({ isShow: value });
    }


    render() {
        if (this.state.isRequested) {
            return <div className="main_fon">
                <div className="inline_block">
                    <div className="text_title_ag">Отчеты по времени: </div>
                </div>
                <ListGroup>
                    <ListGroup.Item className="inline_block">
                        <div className="inline_element">Название задачи</div>
                        <div className="inline_element">Дата создания</div>
                        <div className="inline_element">Название проекта</div>
                        <div className="inline_element">Количество часов</div>
                    </ListGroup.Item>
                    {
                        this.state.timesheets.map(timesheet => (
                            <ListGroup.Item className="inline_block">
                                <a className="inline_element" href={"/task/" + timesheet.task_id}>{timesheet.task_name}</a>
                                <div className="inline_element">
                                    <Moment format="DD.MM.YYYY">
                                        {
                                            new Date(timesheet.date_create)
                                        }
                                    </Moment>
                                </div>
                                <a className="inline_element" href={"/project/" + timesheet.project_id}>{timesheet.project_name}</a>
                                <div className="inline_element">{timesheet.hours_count}</div>
                                <div className="inline_element">
                                    <Button
                                        onClick={() => this.deleteTimesheetHandler(timesheet.timesheet_id)}
                                        className="button_padding"
                                        variant="outline-primary">
                                        Удалить
                                    </Button>
                                </div>
                            </ListGroup.Item>
                        ))
                    }
                </ListGroup>

                <CreateProjectModal show={this.state.isShow}
                    onHide={() => this.setShow(false)}
                    onSave={(name, descr) => this.createNewProjectHandler(name, descr)} />
            </div>;
        } else {
            return <div>Loading...</div>
        }

    };
}

const mapStateToProps = (state) => ({
    user: state.UserReduser,
    timesheets: state.Timesheets,
    isShow: false,
    isRequested: state.Timesheets ? state.Timesheets.IsRequested : false
});

export default connect(
    mapStateToProps,
    null
)(Timesheets);