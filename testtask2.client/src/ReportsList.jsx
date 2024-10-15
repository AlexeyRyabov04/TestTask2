import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';

const ReportsList = () => {
    const [reports, setReports] = useState([]);
    const [selectedReport, setSelectedReport] = useState(null);
    const [file, setFile] = useState(null);

    const loadReports = async () => {
        const response = await fetch('/api/Report/reports');
            if (response.ok) {
                const data = await response.json();
                setReports(data);
            } else {
                console.error('Failed to fetch data');
            }
    }
    const handleFileChange = (e) => {
        setFile(e.target.files[0]);
    };

    const uploadFile = async () => {
        const formData = new FormData();
        formData.append('inputfile', file);

        const response = await fetch('/api/Report', {
            method: 'POST',
            body: formData
        });

        if (response.ok) {
            console.log('File uploaded successfully');
            loadReports();
        } else {
            console.error('File upload failed');
        }
    };
    useEffect(() => {
        loadReports();
    }, []);
    return (
        <div>
        <h1>Reports</h1>
        <ul>
            {reports.map(reports => (
            <li key={reports.id}>
                <Link to={`/${reports.id}`}>{reports.name}</Link>
            </li>
            ))}
        </ul>
        <input type="file" onChange={handleFileChange} />
            <div>
                <button onClick={uploadFile}>Upload File</button>
            </div>
            {selectedReport && (
                <div>
                    <h2>{selectedReport.name}</h2>
                    <p>{selectedReport.description}</p>
                </div>
            )}
        </div>
        
    );
};

export default ReportsList;