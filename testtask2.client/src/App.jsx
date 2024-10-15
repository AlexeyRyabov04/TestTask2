import React, { Component } from 'react';
import { BrowserRouter as Router, Route, Routes, HashRouter } from 'react-router-dom';
import ReportsList from './ReportsList.jsx';
import ReportView from './ReportView.jsx';


function App() {
    return (
        <HashRouter>
            <Routes>
                <Route path="/" element={<ReportsList />} />
                <Route path="/:reportId" element={<ReportView />} />
            </Routes>
        </HashRouter>
    );
}

export default App;