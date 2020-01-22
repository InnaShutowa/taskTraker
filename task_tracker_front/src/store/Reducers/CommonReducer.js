import { createStore, combineReducers } from 'redux';
import UserReducer from './User';
import ProjectsReducer from './Projects'

const reducers = combineReducers({
  user: UserReducer,
  projects: ProjectsReducer
});

const CommonStore = createStore(reducers);

export default CommonStore;