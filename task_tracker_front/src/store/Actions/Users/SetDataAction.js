function SaveDataAction(action) {
    console.log(action);
    return {
        type: "SET_DATA",
        data: action
    };
}

export default SaveDataAction;