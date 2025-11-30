import type {ICategoryItem} from "../../../types/category/ICategoryItem.ts";
import APP_ENV from "../../../env";
import DeleteConfirmDialog from "../../../components/DeleteConfirmDialog";
import {useDeleteCategoryMutation} from "../../../services/apiCategory.ts";
import { Image } from 'antd';

interface Props {
    category: ICategoryItem;
}

const CategoryRow : React.FC<Props> = ({
                                    category,
                                       }) => {
    const [deleteCategory] = useDeleteCategoryMutation();
    const handleDelete = async () => {
        try {
            await deleteCategory(category.id);
        }
        catch (e) {
            console.error("Problem with delete", e);
            alert("Problem with delete");
        }
    }
    return (
        <tr className="bg-white border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200">
            <td className="px-6 py-4">
                <Image src={`${APP_ENV.API_IMAGE_LARGE_URL}${category.image}`}
                     alt={category.name} width={75}/>
            </td>
            <th scope="row"
                className="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                {category.name}
            </th>
            <th>
                <DeleteConfirmDialog
                    title={`Ви дійсно бажаєте видалити ${category.name}?`}
                    deleteAction={handleDelete}
                />
            </th>
        </tr>
    )
}
export default CategoryRow;