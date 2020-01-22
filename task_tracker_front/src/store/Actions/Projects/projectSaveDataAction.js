import Types from '../../Constants';

function ProjectsSaveDataAction(action) {
    return {
        type: Types.Types.projectTypes.PROJECTS_SAVE_DATA_CONST,
        data: action
    };
}

export default ProjectsSaveDataAction;