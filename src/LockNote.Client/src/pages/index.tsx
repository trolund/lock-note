import { useCreateNote } from "../api/client";
import { useQueryClient } from "react-query";
import { NoteDto } from "../types/NoteDto";
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faClipboard } from "@fortawesome/free-solid-svg-icons";
import NoteForm from "../component/note-form";

export const Index = () => {
  const queryClient = useQueryClient();
  const { mutate, data, reset } = useCreateNote(queryClient);

  // copy to clipboard button
  const copyToClipboard = async (data: NoteDto) => {
    if (!navigator.clipboard) {
      return;
    }

    // get base url from window object
    const baseUrl = window.location.origin;
    await navigator.clipboard.writeText(`${baseUrl}/note/${data?.id}`);
  };

  if (data) {
    return (
      <div className="mx-auto flex min-h-8 max-w-2xl flex-col gap-4">
        <button
          type="button"
          data-testid="back-btn"
          onClick={reset}
          className="mt-5 rounded-lg bg-blue-500 px-4 py-2 text-sm font-medium text-white hover:bg-blue-600"
        >
          Back
        </button>
        <p>Your note has been created! You can access it at</p>
        <div className="flex flex-col">
          <Link
            data-testid="note-link"
            to={`note/${data.id}`}
          >{`note/${data.id}`}</Link>
          <button
            type="button"
            data-testid="clipboard-btn"
            className="mt-5 rounded-lg bg-blue-500 px-4 py-2 text-sm font-medium text-white hover:bg-blue-600"
            onClick={() => copyToClipboard(data)}
          >
            <FontAwesomeIcon icon={faClipboard} className="mr-2" />
            Copy to clipboard
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className="block">
      <div className="mx-auto mb-4 min-h-8 max-w-2xl">
        <NoteForm mutate={mutate} />
      </div>
      <div className="rounded-lg bg-gray-900 p-8 shadow-lg">
        <h2 className="mb-4 text-2xl font-bold text-white">
          Lock Note Features
        </h2>
        <div className="flex flex-1 flex-wrap gap-4 lg:flex-nowrap">
          <div className="w-2/4 rounded-lg bg-gray-950 p-4 shadow">
            <h3 className="text-lg font-semibold text-white">Create Notes</h3>
            <p className="text-gray-600">
              Generate a unique link for secure sharing.
            </p>
          </div>
          <div className="w-2/4 rounded-lg bg-gray-950 p-4 shadow">
            <h3 className="text-lg font-semibold text-white">
              One-Time Readability
            </h3>
            <p className="text-gray-600">
              Notes are permanently deleted after being accessed.
            </p>
          </div>
          <div className="w-2/4 rounded-lg bg-gray-950 p-4 shadow">
            <h3 className="text-lg font-semibold text-white">
              Password Protection (Optional)
            </h3>
            <p className="text-gray-600">Add an extra layer of security.</p>
          </div>
          <div className="w-2/4 rounded-lg bg-gray-950 p-4 shadow">
            <h3 className="text-lg font-semibold text-white">
              Expiration Time
            </h3>
            <p className="text-gray-600">
              Notes automatically expire if not accessed. Your note is
              automatically deleted after a month.
            </p>
          </div>
          <div className="w-2/4 rounded-lg bg-gray-950 p-4 shadow">
            <h3 className="text-lg font-semibold text-white">
              End-to-End Encryption
            </h3>
            <p className="text-gray-600">Ensures secure note storage.</p>
          </div>
          <div className="w-2/4 rounded-lg bg-gray-950 p-4 shadow">
            <h3 className="text-lg font-semibold text-white">
              Self-Destruct Mechanism
            </h3>
            <p className="text-gray-600">No data is retained after reading.</p>
          </div>
        </div>
      </div>
    </div>
  );
};
