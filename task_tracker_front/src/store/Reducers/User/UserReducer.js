let initialState = {
    user: [
        {
            id: 1,
            name: 'Сова',
            url: "SOVA_CONST",
            likes: 5,
            currentUserRating: 0,
            date:1557781012,
            user: 'Inna'
        }
    ],
    isOpenModal: false
};

function UserReducer(state = initialState, action) {
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

export default UserReducer;