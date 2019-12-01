let initialState = {
    sort: 0,
    projects: [
        {
            id: 1,
            name: 'Сова',
            url: "SOVA_CONST",
            likes: 5,
            currentUserRating: 0,
            date:1557781012,
            user: 'Inna'
        }
    ]
};

function ProjectsReducer(state = initialState, action) {
    console.log(action.type);
    console.log(handlers[action.type]);
    if (handlers[action.type]){
        return handlers[action.type].handler(state, action);
    }
    return state;
}


const handlers  = {
    "to_do" : {
         handler(state, action){
            return {...state};
        }
    }
};

export default ProjectsReducer;