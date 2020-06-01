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
    ],
    IsRequested: false
};

function ProjectsReducer(state = initialState, action) {

    if (handlers[action.type]) {
        let s = handlers[action.type].handler(state, action);
    }
    return state;
}


const handlers = {
    "PROJECTS_REQUEST": {
        handler(state, action) {
            return { ...state };
        }
    },
    "PROJECTS_SAVE_DATA": {
        handler(state, action) {
            return { projects: action.data, IsRequested: true };
        }
    }
};

export default ProjectsReducer;