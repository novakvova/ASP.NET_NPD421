import './App.css'
import CategoriesListPage from "./pages/categories/CategoriesListPage";
import CategoryCreatePage from "./pages/categories/CategoryCreatePage";
import {Route, Routes} from "react-router";

function App() {

    return (
        <>
            <Routes>
                <Route path={"/"}>
                    <Route index element={<CategoriesListPage/>}/>
                    <Route path={"create"} element={<CategoryCreatePage/>}/>
                </Route>
            </Routes>
        </>
    )
}

export default App
