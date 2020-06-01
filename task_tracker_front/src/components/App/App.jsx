import React from 'react';
import './Styles.css';
import { createStore } from "redux";
import Provider from "react-redux/es/components/Provider";
import { BrowserRouter, Route } from "react-router-dom";
import Header from "../Header";
import CommonStore from "../../store/Reducers"
import Login from '../Login';
import Main from '../Main';
import Projects from '../Projects';
import Project from '../Project';
import Users from '../Users';
import UserPage from '../UserPage';
import Tasks from '../Tasks';
import UserProfile from '../UserProfile/UserProfile';
import Task from '../Task';
import Timesheets from '../Timesheets';
import { PersistGate } from 'redux-persist/integration/react';
import { persistStore, persistReducer, persistCombineReducers } from 'redux-persist';



const isAuth = true;

const persistor = persistStore(CommonStore);
const App = () => {

  var stor = localStorage.getItem('persist:userRoot');
  const decoded = JSON.parse(stor);

  console.log(decoded);
  let header = <Route exact path={""} component={Login} />;
  let users = <Route exact path={""} component={Main} />;
  let userPage = <Route exact path={""} component={Main} />;
  let projects = <Route exact path={""} component={Main} />;
  let project = <Route exact path={""} component={Main} />;
  let userProfile = <Route exact path={""} component={Main} />;
  let tasks = <Route exact path={""} component={Main} />;
  let task = <Route exact path={""} component={Main} />;
  let timesheets = <Route exact path={""} component={Main} />;

  if (decoded && decoded.apikey) {
    header = <Route exact path={""} component={Header} />;
    userProfile = <Route exact path={"/userProfile"} component={UserProfile} />;
    tasks = <Route exact path={"/tasks"} component={Tasks} />;
    task = <Route path={"/task"} component={Task} />;
    timesheets = <Route path={"/timesheets"} component={Timesheets} />;
    if (decoded.is_admin) {
      users = <Route exact path={"/users"} component={Users} />;
      projects = <Route exact path={"/projects"} component={Projects} />;
      project = <Route path={"/project/"} component={Project} />;
      userPage = <Route path={"/userPage"} component={UserPage} />;
    }
  }
  return <body className="back">
    <Provider store={CommonStore}>
      <PersistGate loading={null} persistor={persistor}>
        <BrowserRouter>
          <div>
            {header}
            {users}
            {projects}
            {project}
            {userProfile}
            {tasks}
            {task}
            {timesheets}
            {userPage}
          </div>
        </BrowserRouter>
      </PersistGate>
    </Provider>
  </body>;
};

export default App;
