// import {useEffect, useState} from "react";
// import type {ICategoryItem} from "../../../types/category/ICategoryItem.ts";
import CategoryRow from "./CategoryRow.tsx";
import {useGetCategoriesQuery} from "../../../services/apiCategory.ts";
// import api from "../../../api";

const CategoriesListPage = () => {

    // const [categories, setCategories] = useState<ICategoryItem[]>([]);
    //
    // useEffect(() => {
    //     requestCategories();
    // },[]);
    //
    // const requestCategories = async () => {
    //     try {
    //         const response =
    //             await api.get<ICategoryItem[]>(`categories`);
    //         const { data } = response;
    //         setCategories(data);
    //     }
    //     catch (e) {
    //         console.error("Propblem working requewstCategories", e);
    //     }
    // }

    const {data: categories} = useGetCategoriesQuery();
    // console.log("loading", isLoading);
    // console.log("error", error);

    return (
        <>
            <h1 className={"text-4xl font-extrabold dark:text-white text-center"}>Список категорій</h1>


            <div className="relative overflow-x-auto">
                <table className="w-full text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400">
                    <thead className="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                    <tr>
                        <th scope="col" className="px-6 py-3">
                            Фото
                        </th>
                        <th scope="col" className="px-6 py-3">
                            Назва
                        </th>
                        <th scope="col" className="px-6 py-3">

                        </th>
                    </tr>
                    </thead>
                    <tbody>
                    {categories?.map((category) =>
                        <CategoryRow key={category.id} category={category} />
                    )}
                    </tbody>
                </table>
            </div>

        </>
    );
}

export default CategoriesListPage;