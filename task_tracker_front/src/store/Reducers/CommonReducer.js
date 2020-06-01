import { createStore, combineReducers } from 'redux';
import UserReducer from './User';
import ProjectsReducer from './Projects';
import {persistStore, persistReducer, persistCombineReducers} from 'redux-persist';
import storage from 'redux-persist/lib/storage';


const persistUserConfig = {
  key: 'userRoot',
  storage,
};

const persistConfig = {
  key: 'root',
  storage,
};

const persistedUserReducer = persistReducer(persistUserConfig, UserReducer);
console.log(persistedUserReducer);
const reducers = {
  UserReduser: persistedUserReducer
};


const reducersPersist = persistCombineReducers(persistConfig, reducers);

const CommonStore = createStore(reducersPersist);

export default CommonStore;