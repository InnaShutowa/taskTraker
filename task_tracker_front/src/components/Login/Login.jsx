import React from 'react';
import * as css from "./Styles.css";

const Login = () => {
    return <div className="auth-window">
        <div className="container">
            <div className="row">
                <div className="col-md-offset-3 col-md-6">
                    <form className="form-horizontal">
                        <span className="heading">АВТОРИЗАЦИЯ</span>
                        <div className="form-group">
                            <input type="email" className="form-control" id="inputEmail" placeholder="E-mail" />
                            <i className="fa fa-user"></i>
                        </div>
                        <div className="form-group help">
                            <input type="password" className="form-control" id="inputPassword" placeholder="Password" />
                            <i className="fa fa-lock"></i>
                        </div>
                        <div className="form-group button-padding">
                            <div className="main-checkbox">
                                <input type="checkbox" value="none" id="checkbox1" name="check" />
                                
                            </div>
                            <span className="text">Запомнить</span>
                            <button type="submit" className="btn btn-default">ВХОД</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
}

export default Login;