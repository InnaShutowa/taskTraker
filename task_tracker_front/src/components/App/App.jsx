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
import UserProfile from '../UserProfile/UserProfile';



const isAuth = true;
let header =  <Route exact path={""} component={Main} />;
if (isAuth) {
  header = <Route exact path={""} component={Header} />;
}

const App = () => {
  return <body className="back">
    <Provider store={CommonStore}>
      <BrowserRouter>
        <div>
           {header}
          <Route exact path={"/login"} component={Login} />
          <Route exact path={"/projects"} component={Projects} />
          <Route path={"/project/"} component={Project} />
          <Route exact path={"/users"} component={Users} />
          <Route exact path={"/userProfile"} component={UserProfile} />


          {/* <Route exact path={""} component={Header}/>
                <Route exact path={"/registration"} component={RegistrationCreateNickname}/>
                <Route exact path={"/registrationnext"} component={RegistrationFullInfo}/>
                <Route exact path={"/registrationpass"} component={RegistrationPassword}/>
                <Route exact path={"/authorize"} component={Authorization}/>
                <Route exact path={"/main"} component={Main}/> */}
                
        </div>
      </BrowserRouter>
    </Provider>
  </body>;
};

export default App;
