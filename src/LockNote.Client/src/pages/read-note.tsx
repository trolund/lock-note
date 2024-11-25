import { useParams } from "react-router-dom";
import { useGetNoteById } from "../api/client";

export const ReadNote = () => {
  const { noteId } = useParams();

  if (!noteId) {
    return <h1>Invalid noteId</h1>;
  }

  const { data, isLoading } = useGetNoteById(noteId);

  return (
    <div>
      <h1>{noteId}</h1>
      {isLoading ? (
        <p>Loading note...</p>
      ) : (
        <p>
          <textarea
            id="message"
            title="note content"
            className="block p-2.5 w-full text-sm text-gray-900 bg-gray-50 rounded-lg border border-gray-300 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
            readOnly
            disabled
          >
            {data?.content}
          </textarea>
        </p>
      )}
    </div>
  );
};
