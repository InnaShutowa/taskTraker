let initialState = {
    projects: [
        {
            id: 1,
            title: "Yee",
            description: "!!!!",
            dateCreate: "12.01.2019",
            countUsers: 10,
            creator: "Барабашка"
        }
    ]
};

function ProjectsReducer(state = initialState, action) {

    if (handlers[action.type]){
        return handlers[action.type].handler(state, action);
    }
    return state;
}


const handlers  = {
    "PROJECTS_REQUEST" : {
         handler(state, action){
            return {...state};
        }
    },
    "PROJECTS_SAVE_DATA" : {
        hander(state, action) {
            return {projects: action};
        }
    }
};

export default ProjectsReducer;