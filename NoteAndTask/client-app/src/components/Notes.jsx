import React from 'react';
import {SketchField, Tools} from 'react-sketch';
import "./Notes.css";
import {BoardLayout} from "./layout/BoardLayout";
import {DashBoard} from "./layout/DashBoard";

export class Notes extends React.Component {
    static displayName = Notes.name;

    constructor(props) {
        super(props);
        this.state = {notes: null, loading: false};

        // request('/api/Task/Tasks?archived=true')
        //     .then(data => {
        //         this.setState({tasks: data, loading: true})
        //     });
    }

    // renderTasks (tasks) {
    //     return (
    //         <div className="row">
    //             {tasks.map(task =>
    //                 <div className="card m-3 shadow-sm" key={task.taskId}>
    //                     <div className="card-header">
    //                         <h6><i className="fas fa-thumbtack mr-2"/>{task.name}</h6>
    //                     </div>
    //                     <div className="card-body">
    //                         <p className="card-text">{task.description}</p>
    //                     </div>
    //                     <div className="card-footer">
    //                         <div className="small">
    //                             Created: {task.creationDate}
    //                         </div>
    //                         <div className="small">
    //                             <span>Expires:</span> {task.expiresOn}
    //                         </div>
    //                     </div>
    //                 </div>
    //             )}
    //         </div>
    //     );
    // }

    render() {
        return (
            <DashBoard>
                <SketchField width='1024px'
                             height='768px'
                             tool={Tools.Pencil}
                             lineColor='black'
                             lineWidth={1}/>
            </DashBoard>
        );
    }
}

{/*<BoardLayout>*/}
{/*    <SketchField width='1024px'*/}
{/*                 height='768px'*/}
{/*                 tool={Tools.Pencil}*/}
{/*                 lineColor='black'*/}
{/*                 lineWidth={1}/>*/}
{/*</BoardLayout>*/}